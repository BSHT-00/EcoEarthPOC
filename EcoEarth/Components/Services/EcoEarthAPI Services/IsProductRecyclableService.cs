using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EcoEarthPOC.Components.Pages.Scanner.Data.DTOs;


// This class communicates with the recyclable materials table
namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    public class IsProductRecyclableService
    {
        // https://localhost:7111/api/RecyclableMaterials
        public static string ServiceBaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7111/api" : "https://localhost:7111/api";
        const string Endpoint = "/RecyclableMaterials";
        private readonly HttpClient _httpClient;

        public IsProductRecyclableService(HttpClient httpClient)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(handler);
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Returns a list of recycable materials
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