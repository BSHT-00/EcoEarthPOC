﻿using EcoEarthPOC.Components.Pages.Scanner.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// Service which handles requests to and from the open food facts api
namespace EcoEarthPOC.Components.Services
{
    // This service handles calls to the OpenFoodFacts API
    public class OFFPackaging
    {
        // Attatch Barcode number to the end of this for details about product.
        // E.g. https://en.openfoodfacts.org/api/v0/product/6111035000430.json?fields=packaging
        const string API_URL_Packaging = "https://en.openfoodfacts.org/api/v0/product/{0}.json?fields=packaging";
        const string API_URL_MoreInfo = "https://en.openfoodfacts.org/api/v0/product/{0}.json?fields=packaging,product,selected_images,brands,packagings,product_name_en";

        private readonly HttpClient _httpClient;

        public OFFPackaging(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<GetPackagingInfoDTO> GetPackagingInformation(string productCode)
        {
            var url = string.Format(API_URL_Packaging, productCode);

            var response = await _httpClient.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var item = JsonSerializer.Deserialize<GetPackagingInfoDTO>(jsonResponse, jsonOptions);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(url), "The EventType response is null");
            }

            return item;
        }

        // Retrieves more detailed info about a product (used for product info page)
        public async Task<GetMorePackagingInfoDTO> GetMorePackagingInformation(string productCode)
        {
            var url = string.Format(API_URL_MoreInfo, productCode);

            var response = await _httpClient.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var item = JsonSerializer.Deserialize<GetMorePackagingInfoDTO>(jsonResponse, jsonOptions);

            if (item == null)
            {
                throw new Exception("Product could not be found");
            }

            return item;
        }


    }
}
