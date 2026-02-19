using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Models;
using System.Text;
using System.Text.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeminiController : ControllerBase
    {

        [HttpGet("prompt/{textPrompt}")]
        public async Task<IActionResult> GetPromt(string textPrompt)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables()
                      .Build();

                var apiKey = configuration["ApiKeyGemini"];
                var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + apiKey;

                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = textPrompt }
                            }
                        }
                    }
                };

                var json = JsonSerializer.Serialize(payload);

                using var client = new HttpClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(result);

                var texto = doc.RootElement
                   .GetProperty("candidates")[0]
                   .GetProperty("content")
                   .GetProperty("parts")[0]
                   .GetProperty("text")
                   .GetString();

                return Ok(texto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }

        [HttpGet("actividad-metadata")]
        [Produces("application/json")]
        public async Task<ActionResult<Actividad>> GetActividadMetadata(
            [FromQuery] string imageUrl,
            CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return BadRequest("El parámetro 'imageUrl' es obligatorio.");

            byte[] bytes;
            string? contentType;

            using (var httpClient = new HttpClient())
            using (var resp = await httpClient.GetAsync(imageUrl, ct))
            {
                if (!resp.IsSuccessStatusCode)
                    return BadRequest("No se pudo descargar la imagen.");

                contentType = resp.Content.Headers.ContentType?.MediaType;
                bytes = await resp.Content.ReadAsByteArrayAsync(ct);
            }

            var mimeType = DetectMime(contentType, bytes);

            if (mimeType == null)
                return BadRequest("Formato de imagen no soportado.");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var apiKey = configuration["ApiKeyGemini"];

            if (string.IsNullOrWhiteSpace(apiKey))
                return StatusCode(500, "Falta configurar ApiKeyGemini.");

            var endpoint =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            var base64 = Convert.ToBase64String(bytes);

            var prompt = """
Eres un asistente experto en deportes.

Analiza la imagen y genera información estructurada sobre la actividad deportiva que aparece.

Devuelve:

- Descripcion: explicación clara de la actividad (máx 80 palabras)
- EdadRecomendada: rango etario sugerido (ej: "6 a 12 años", "Adultos")
- Beneficios: beneficios físicos y mentales principales

Si no estás seguro, infiere razonablemente.
Devuelve SOLO JSON válido.
""";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new object[]
                        {
                            new { text = prompt },
                            new
                            {
                                inline_data = new
                                {
                                    mime_type = mimeType,
                                    data = base64
                                }
                            }
                        }
                    }
                },
                generationConfig = new
                {
                    response_mime_type = "application/json"
                }
            };

            using var http = new HttpClient();
            using var msg = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json")
            };

            var response = await http.SendAsync(msg, ct);
            var json = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, json);

            using var doc = JsonDocument.Parse(json);

            var jsonPayload =
                doc.RootElement
                   .GetProperty("candidates")[0]
                   .GetProperty("content")
                   .GetProperty("parts")[0]
                   .GetProperty("text")
                   .GetString();

            if (string.IsNullOrWhiteSpace(jsonPayload))
                return StatusCode(502, "No se recibió JSON del modelo.");

            ActivityMetaDataDTO? metadata;

            try
            {
                metadata = JsonSerializer.Deserialize<ActivityMetaDataDTO>(
                    jsonPayload,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                return StatusCode(502, $"Error parseando JSON: {jsonPayload}");
            }

            var actividad = new Actividad
            {
                Descripcion = metadata?.Descripcion ?? "",
                EdadRecomendada = metadata?.EdadRecomendada ?? "",
                Beneficios = metadata?.Beneficios ?? ""
            };

            return Ok(actividad);
        }

        // 🔹 DETECCIÓN MIME
        private static string? DetectMime(string? contentType, byte[] bytes)
        {
            if (!string.IsNullOrWhiteSpace(contentType))
            {
                var ct = contentType.Split(';')[0].Trim().ToLowerInvariant();
                if (ct is "image/jpeg" or "image/jpg" or "image/png" or "image/webp")
                    return ct == "image/jpg" ? "image/jpeg" : ct;
            }

            // JPEG
            if (bytes.Length > 3 &&
                bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
                return "image/jpeg";

            // PNG
            if (bytes.Length > 8 &&
                bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47 &&
                bytes[4] == 0x0D && bytes[5] == 0x0A && bytes[6] == 0x1A && bytes[7] == 0x0A)
                return "image/png";

            // WEBP
            if (bytes.Length > 12 &&
                bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46 &&
                bytes[8] == 0x57 && bytes[9] == 0x45 && bytes[10] == 0x42 && bytes[11] == 0x50)
                return "image/webp";

            return null;
        }
    }
}
