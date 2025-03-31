using EcoEarthPOC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Services
{
    public class AppService : IAppService
    {
        private string _baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:44371" : "https://localhost:44371";
        private readonly HttpClient _httpClient;

        // Http Client injected and handler bypasses SSL (which was preventing the API from working)
        public AppService(HttpClient httpClient)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(handler);
        }

        public async Task<string> AuthenticateUser(LoginModel loginModel)
        {
            string returnStr = string.Empty;

            var serializedData = JsonConvert.SerializeObject(loginModel);

            var response = await _httpClient.PostAsync($"{_baseUrl}{APIs.AuthenticateUser}", new StringContent(serializedData, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                returnStr = await response.Content.ReadAsStringAsync();
            }

            return returnStr;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerModel)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}{APIs.RegisterUser}";

                var serializedData = JsonConvert.SerializeObject(registerModel);

                var response = await client.PostAsync(url, new StringContent(serializedData, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    isSuccess = true;
                }
                else
                {
                    errorMessage = await response.Content.ReadAsStringAsync();
                }
            }
            return (isSuccess, errorMessage);
        }
    }
}
