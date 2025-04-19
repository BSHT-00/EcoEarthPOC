using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// This service handles the login process for the EcoEarth API
// The LoginAPI uses a hash to identify users but the EEAPI uses an int
// This service will allow the 2 services to communicate through the use of the Login table
namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    public class LoginEEService
    {
        //https://localhost:7111/api/login

        // Base URL that changes based on platform. Android uses 10.0.2.2:<port> to access localhost api
        public static string ServiceBaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7111/api" : "https://localhost:7111/api";
        const string Endpoint = "/login";
        private readonly HttpClient _httpClient;

        //httpClient bypasss
        public LoginEEService(HttpClient httpClient)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(handler);
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Example URL: https://localhost:7111/api/login?loginId=loginId
        // Creates a new account in the EcoEarth API, returns the userId so that the user can be logged in
        public async Task<int> CreateNewAccount(string loginId)
        {
            var response = await _httpClient.PostAsync($"{ServiceBaseUrl}{Endpoint}?loginId={loginId}", null);

            response.EnsureSuccessStatusCode();

            // Read the response content as a string
            var responseContent = await response.Content.ReadAsStringAsync();

            return int.Parse(responseContent);
        }

        // https://localhost:7111/api/login/loginid
        // Logging a user in to the EcoEarth API, will require receiving the userId using loginId
        public async Task<int> Login(string loginId)
        {
            var response = await _httpClient.GetAsync($"{ServiceBaseUrl}{Endpoint}/{loginId}");
            response.EnsureSuccessStatusCode();
            // Read the response content as a string
            var responseContent = await response.Content.ReadAsStringAsync();
            return int.Parse(responseContent);
        }



    }
}
