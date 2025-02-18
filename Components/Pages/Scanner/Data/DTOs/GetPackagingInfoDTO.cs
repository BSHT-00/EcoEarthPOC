using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EcoEarthPOC.Components.Pages.Scanner.Data.DTOs
{
    // Uses the OFF API to get packaging information about a product
    public class GetPackagingInfoDTO
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("packaging")]
        public string Packaging { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("status_verbose")]
        public string StatusVerbose { get; set; }
    }
}
