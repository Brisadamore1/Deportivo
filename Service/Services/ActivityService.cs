using Microsoft.Extensions.Caching.Memory;
using Service.DTOs;
using Service.Interfaces;
using Service.Models;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ActivityService : GenericService<Actividad>, IActivityService
    {
        public ActivityService(HttpClient? httpClient = null, IMemoryCache? memoryCache = null) : base(httpClient, memoryCache)
        {

        }

        public async Task<List<Actividad>?> GetWithFilterAsync(FilterActivityDTO filter)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/withfilter",filter);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener los datos: {response.StatusCode}");
            }
            return JsonSerializer.Deserialize<List<Actividad>>(content, _options);
        }

    }
}
