using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Pages.Scanner.Data.DTOs
{
    // DTO for the IsItRecyclable endpoint. retrieves list of these items from API
    public class IsItRecyclableDTO
    {
        [JsonProperty("Material")]
        public string Material { get; set; }
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
    }
}
