using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    public class IsProductRecyclableService
    {
        const string ServiceBaseUrl = "http://localhost:5250/api";
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
    }
}