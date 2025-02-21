using AuthenticationServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EcoEarthPOC.Components.Pages.Scanner.Data.DTOs
{
    // This DTO is used to transfer more detailed data about a product (Image, name, etc)
    public class GetMorePackagingInfoDTO
    {
            public string Brand { get; set; }
            public string ImageUrl { get; set; }
            public string Packaging { get; set; }
            public string ProductName { get; set; }
            public Dictionary<string, ImageUrls> SelectedImages { get; set; }
        

        public class ImageUrls
        {
            public string Display { get; set; }
            public string Small { get; set; }
            public string Thumb { get; set; }
        }
    }
}
