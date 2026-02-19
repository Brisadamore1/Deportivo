using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Pgvector;
using Service.Interfaces;
using Service.Models;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient = new HttpClient();
        protected readonly JsonSerializerOptions _options;
        public static string? jwtToken = string.Empty;
        private readonly IMemoryCache? _memoryCache;

        public GeminiService(IConfiguration configuration, IMemoryCache? memoryCache = null)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            //if (!string.IsNullOrEmpty(GenericService<object>.jwtToken))
            //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenericService<object>.jwtToken);
            //else
            //    throw new ArgumentException("Error Token no definido", nameof(GenericService<object>.jwtToken));

        }
        protected void SetAuthorizationHeader()
        {
            // Si ya está configurado (por un DelegatingHandler), no hacer nada
            if (_httpClient.DefaultRequestHeaders.Authorization is not null)
                return;

            // 1) Intentar leer desde IMemoryCache (configurado por FirebaseAuthService)
            if (_memoryCache is not null && _memoryCache.TryGetValue("jwt", out string? cachedToken) && !string.IsNullOrWhiteSpace(cachedToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cachedToken);
                return;
            }

            // 2) Respaldo: variable estática (evitar uso si no es necesario)
            if (!string.IsNullOrWhiteSpace(GenericService<object>.jwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenericService<object>.jwtToken);
                return;
            }
            // Si no se definió el token, se lanza una excepción

            //throw new InvalidOperationException("El token JWT no está disponible para la autorización.");





        }
        public async Task<string?> GetPrompt(string textPrompt)
        {
            SetAuthorizationHeader();
            if (string.IsNullOrEmpty(textPrompt))
            {
                throw new ArgumentException("El texto del prompt no puede estar vacío.", nameof(textPrompt));
            }
            try
            {
                var urlApi = _configuration["UrlApi"];
                var endpointGemini = ApiEndpoints.GetEndpoint("Gemini");
                
                var response = await _httpClient.GetAsync($"{urlApi}{endpointGemini}/prompt/{textPrompt}");
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception($"Error en la respuesta de la API: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el prompt de Gemini: " + ex.Message);
            }




        }

        public async Task<Actividad?> GetActividadFromImagen(string imageUrl)
        {
            SetAuthorizationHeader();

            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentException("La url de la imagen no puede estar vacía.", nameof(imageUrl));
            }

            try
            {
                var urlApi = _configuration["UrlApi"];
                var endpointGemini = ApiEndpoints.GetEndpoint("Gemini");

                // Limpio la URL
                imageUrl = Uri.EscapeDataString(imageUrl);

                var response = await _httpClient.GetAsync(
                    $"{urlApi}{endpointGemini}/actividad-metadata?imageUrl={imageUrl}"
                );

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    // Esperamos JSON con:
                    // {
                    //   "descripcion": "...",
                    //   "edadRecomendada": "...",
                    //   "beneficios": "..."
                    // }

                    var metadata = JsonSerializer.Deserialize<Actividad>(result, _options);

                    if (metadata != null)
                    {
                        return metadata;
                    }

                    return null;
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en actividad-metadata: {response.StatusCode} - {errorContent}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener metadata de actividad: {ex.Message}");
            }
        }


        public async Task<float[]> CrearEmbeddingAsync(string texto)
        {
            SetAuthorizationHeader();
            if (string.IsNullOrEmpty(texto))
            {
                throw new ArgumentException("El texto no puede estar vacío.", nameof(texto));
            }
            try
            {
                var urlApi = _configuration["UrlApi"];
                var endpointGemini = ApiEndpoints.GetEndpoint("Gemini");
                var response = await _httpClient.GetAsync($"{urlApi}{endpointGemini}/embed?texto={Uri.EscapeDataString(texto)}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var embedding = JsonSerializer.Deserialize<float[]>(result, _options);
                    if (embedding != null)
                    {
                        return embedding;
                    }
                    else
                    {
                        throw new Exception("El embedding recibido es nulo.");
                    }
                }
                else
                {
                    throw new Exception($"Error en la respuesta de la API: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el embedding: " + ex.Message);
            }
        }
    }
}
