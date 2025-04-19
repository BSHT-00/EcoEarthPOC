using EcoEarthPOC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EcoEarthPOC.Components.Services.EcoEarthAPI_Services;
using System.IdentityModel.Tokens.Jwt;

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

            var serializedData = JsonConvert.SerializeObject(registerModel);

            var response = await _httpClient.PostAsync($"{_baseUrl}{APIs.RegisterUser}", new StringContent(serializedData, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                // Get account hash
                var email = registerModel.Email;
                var loginId = await GetUserId(email);

                if (loginId == "Not Found" || loginId == null)
                {
                    throw new Exception("Something went wrong registering your account");
                }

                // Use LoginEEService to register user in other db (implementation not provided)
                LoginEEService loginEEService = new LoginEEService(_httpClient);
                var loginResponse = await loginEEService.CreateNewAccount(loginId);

                isSuccess = true;
            }
            else
            {
                errorMessage = await response.Content.ReadAsStringAsync();
            }

            return (isSuccess, errorMessage);
        }

        // https://localhost:44371/api/Users/GetUser/b%40t.com
        //{"userId":"6ff8b9a7-adbc-402a-b53a-f7d6fd071aee"}
        // Gets a user's loginId using email
        public async Task<string> GetUserId(string email)
        {
            email = email.Replace("@", "%40");

            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Users/GetUser/{email}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                content.Split('"');
                return content.Split('"')[3];
            }
            return "Not Found";
        }
    }
}
