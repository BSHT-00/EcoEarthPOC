using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    //handles dailyStreak logic
    class DailyStreakService
    {
        //https://localhost:7111/api/DailyStreak

        // Base URL that changes based on platform. Android uses 10.0.2.2:<port> to access localhost api
        public static string ServiceBaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7111/api" : "https://localhost:7111/api";
        const string Endpoint = "/DailyStreak";
        private readonly HttpClient _httpClient;

        //httpClient bypasss
        public DailyStreakService(HttpClient httpClient)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(handler);
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        //Get users streak
        public async Task<int> GetUserStreak()
        {
            var response = await _httpClient.GetAsync($"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(content, jsonOptions);
            }
            return 0;
        }

        //remove users streak
        public async Task RemoveStreak()
        {
            var url = $"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}";

            var response = await _httpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to remove daily streak");
            }
        }

        //check users streak
        //removes streak if not updated in over a day
        public async Task CheckStreak()
        {
            var url = $"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}";

            var response = await _httpClient.GetAsync($"{url}/LastScanDate");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var lastScanDate = JsonSerializer.Deserialize<DateTime>(content, jsonOptions);

                if (lastScanDate < DateTime.UtcNow.Date.AddDays(-1))
                {
                    await RemoveStreak();
                }
            }
            else
            {
                throw new Exception("Failed to check last scan date");
            }
        }

        //updates users streak and LastScanDate
        public async Task UpdateStreak()
        {
            var url = $"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}";

            var response = await _httpClient.GetAsync($"{url}/LastScanDate");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var lastScanDate = JsonSerializer.Deserialize<DateTime>(content, jsonOptions);

                if (lastScanDate.Date < DateTime.UtcNow.Date)
                {
                    var update = await _httpClient.PutAsync(url, null);
                    if (!update.IsSuccessStatusCode)
                    {
                        throw new Exception("Failed to add day to daily streak");
                    }

                    var updateLastScan = await _httpClient.PutAsync($"{url}/UpdateScanDate", null); 
                    if (!update.IsSuccessStatusCode)
                    {
                        throw new Exception("Failed to update lastScanDate");
                    }
                }
            }
            else
            {
                throw new Exception("Failed to check last scan date");
            }
        }
    }
}
    
