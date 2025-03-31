using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Models
{
    public class UserBasicDetail
    {
        public string Name { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }   
}
