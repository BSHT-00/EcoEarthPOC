using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EcoEarthPOC.Components.Pages.UserTickets.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    // This service handles ticket calls
    public class UserTicketsService
    {
        public static string ServiceBaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7111/api" : "https://localhost:7111/api";
        const string Endpoint = "/UserTickets";
        private readonly HttpClient _httpClient;

        public UserTicketsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Posts a ticket
        public async Task<bool> PostTicketAsync(UserTicketsDTO userTicket)
        {
            var url = $"{ServiceBaseUrl}{Endpoint}";
            var response = await _httpClient.PostAsJsonAsync(url, userTicket);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<UserTicketsDTO>(jsonResponse, jsonOptions);

            // Null check
            if (item == null)
            {
                return false;
            }

            // Indicates successful post
            return true;
        }
    }
}
