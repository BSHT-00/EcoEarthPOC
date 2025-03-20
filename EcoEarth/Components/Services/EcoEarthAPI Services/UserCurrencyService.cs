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
        // https://localhost:7111/api/RecyclableMaterials
        const string ServiceBaseUrl = "https://localhost:7111/api";
        const string Endpoint = "/UserCurrency";
        private readonly HttpClient _httpClient;

        public UserCurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<int> GetUserBalance(int userId)
        {
            var response = await _httpClient.GetAsync($"{ServiceBaseUrl}{Endpoint}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(content, jsonOptions);
            }
            return 0;
        }

        public async Task<int> AddCurrency(int userId, int currency)
        {
            var response = await _httpClient.PostAsync($"{ServiceBaseUrl}{Endpoint}/{userId}", new StringContent(JsonSerializer.Serialize(currency), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(content, jsonOptions);
            }
            return 0;
        }

        public async Task<int> RemoveCurrency(int userId, int currency)
        {
            var response = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{Endpoint}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(content, jsonOptions);
            }
            return 0;
        }
    }
}
