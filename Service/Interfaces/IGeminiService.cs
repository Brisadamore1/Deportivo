using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGeminiService
    {
        Task<string?> GetPrompt(string textPrompt);
        Task<Actividad?> GetActividadFromImagen(string imageUrl);
        Task<float[]> CrearEmbeddingAsync(string texto);
    }
}
