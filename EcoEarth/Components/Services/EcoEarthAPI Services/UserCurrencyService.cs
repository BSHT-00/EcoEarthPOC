using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    // TODO: Test
    public class UserCurrencyService
    {
        // https://localhost:7111/api/RecyclableMaterials (windows)
        // https://10.0.2.2:7111/api/UserCurrency/1 (android)

        // Base URL that changes based on platform. Android uses 10.0.2.2:<port> to access localhost api
        public static string ServiceBaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7111/api" : "https://localhost:7111/api";
        const string Endpoint = "/UserCurrency"; 
        private readonly HttpClient _httpClient;

        // Http Client injected and handler bypasses SSL (which was preventing the API from working)
        public UserCurrencyService(HttpClient httpClient)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(handler);
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<int> GetUserBalance()
        {
            var response = await _httpClient.GetAsync($"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(content, jsonOptions);
            }
            return 0;
        }

        public async Task AddCurrency(int currency)
        {
            var url = $"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}/{currency}";

            var response = await _httpClient.PutAsync(url, null);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add currency");
            }
        }

        public async Task RemoveCurrency(int currency)
        {
            var url = $"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}/{currency}";

            var response = await _httpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add currency");
            }
        }
    }
}
