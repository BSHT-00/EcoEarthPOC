using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Backend_Stuff.Tables
{
    public class UserCurrency
    {
        [Required]
        private int UserId;

        [Required]
        private int? Currency;
    }
}
