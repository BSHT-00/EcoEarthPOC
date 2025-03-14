using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EcoEarthPOC.Components.Pages.Scanner.Data.DTOs;

namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    public class IsProductRecyclableService
    {
        // https://localhost:7111/api/RecyclableMaterials
        const string ServiceBaseUrl = "https://localhost:7111/api";
        const string Endpoint = "/RecyclableMaterials";
        private readonly HttpClient _httpClient;

        public IsProductRecyclableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<IsItRecyclableDTO>> GetRecyclableMaterials()
        {
            try
            {
                // Throwing an exception
                var response = await _httpClient.GetAsync(ServiceBaseUrl + Endpoint);
                var content = await response.Content.ReadAsStringAsync();
                var recyclableMaterials = JsonSerializer.Deserialize<List<IsItRecyclableDTO>>(content, jsonOptions);
                return recyclableMaterials;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Data);
                return null;
            }

            
        }
    }
}