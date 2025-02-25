using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcoEarthPOC.Components.Pages.Scanner.Data.DTOs
{
    public class GetMorePackagingInfoDTO
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("status_verbose")]
        public string StatusVerbose { get; set; }

        [JsonPropertyName("product")]
        public Product ProductInfo { get; set; }

        public class Product
        {
            [JsonPropertyName("brands")]
            public string Brand { get; set; }

            [JsonPropertyName("packagings")]
            public List<Packagings> DetailedPackaging { get; set; }

            [JsonPropertyName("selected_images")]
            public Dictionary<string, ImageUrls> SelectedImages { get; set; }

            [JsonPropertyName("product_name_en")]
            public string ProductName { get; set; }
        }

        public class ImageUrls
        {
            [JsonPropertyName("display")]
            public Dictionary<string, string> Display { get; set; }

            [JsonPropertyName("small")]
            public Dictionary<string, string> Small { get; set; }

            [JsonPropertyName("thumb")]
            public Dictionary<string, string> Thumb { get; set; }
        }

        public class Packagings
        {
            [JsonPropertyName("material")]
            public string Material { get; set; }

            [JsonPropertyName("shape")]
            public string Shape { get; set; }
        }
    }
}
