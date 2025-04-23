using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EcoEarthPOC.Components.Pages.Quests;
using EcoEarthPOC.Components.Pages.Quests.DTOs;

namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    internal class QuestService
    {
        //https://localhost:7111/api/Quest

        // Base URL that changes based on platform. Android uses 10.0.2.2:<port> to access localhost api
        public static string ServiceBaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7111/api" : "https://localhost:7111/api";
        const string Endpoint = "/Quest";
        private readonly HttpClient _httpClient;

        //httpClient bypasss
        public QuestService(HttpClient httpClient)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(handler);
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        //Get users quests
        public async Task<IEnumerable<QuestDTO>> GetAllQuests()
        {
            var response = await _httpClient.GetAsync
                ($"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}/GetAllQuests");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<QuestDTO>>(content, jsonOptions);
            }
            return Array.Empty<QuestDTO>();
        }

        //Get specied quest
        public async Task<QuestDTO> GetQuest(int questId)
        {
            var response = await _httpClient.GetAsync
                ($"{ServiceBaseUrl}{Endpoint}/{questId}/GetQuest");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var quest = JsonSerializer.Deserialize<QuestDTO>(content, jsonOptions);
                return quest;
            }
            return null;
        }

        //Update quest progress
        public async Task UpdateQuestProgress(int questId)
        {
            var url = $"{ServiceBaseUrl}{Endpoint}/{questId}/UpdateProgress";

            var response = await _httpClient.PutAsync(url, null);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update quest progress");
            }
        }

        //assign quests and reset
        public async Task AssignQuests()
        {
            var url = ($"{ServiceBaseUrl}{Endpoint}/{AppVariables.UserId}");
            var quests = await GetAllQuests();
            var questDate = quests.FirstOrDefault()?.LastLoginDate;

            //deletes quests every new day
            if (questDate != DateTime.UtcNow.Date && questDate != null)
            {
                var delete = await _httpClient.DeleteAsync($"{url}/DeleteQuests");
                if (!delete.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to remove quests");
                }
            }
            //assigns 1 major quest
            var majorQuest = await _httpClient.PostAsync(($"{url}/AssignMajorQuest"), null);
            if (!majorQuest.IsSuccessStatusCode)
            {
                throw new Exception("Failed to assign major quest");
            }
            //assigns 2 minor quests
            for (int i = 0; i < 2; i++)
                {
                var minorQuest = await _httpClient.PostAsync(($"{url}/AssignMinorQuest"), null);
                if (!minorQuest.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to assign minor quest");
                }
            }
        }
    }
}
