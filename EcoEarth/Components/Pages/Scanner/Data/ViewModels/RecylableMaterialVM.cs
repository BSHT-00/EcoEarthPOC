using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Pages.Scanner.Data.ViewModels
{
    public class RecylableMaterialVM
    {
        public string Material { get; set; }
        public string Shape { get; set; }
        public bool Recyclable { get; set; }
    }
}
