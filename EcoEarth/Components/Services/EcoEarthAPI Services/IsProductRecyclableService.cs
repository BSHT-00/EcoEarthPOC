using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Services.EcoEarthAPI_Services
{
    public class IsProductRecyclableService
    {
        private readonly HttpClient _httpClient;

        public IsProductRecyclableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsProductRecyclableAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));
            }

            var response = await _httpClient.GetAsync($"https://api.ecoearthapp.com/recyclable/{productId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return bool.Parse(content);
        }
    }
}