using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.Tables
{
    // A table which holds recyclable materials and a category which will be used to direct users to relevant resources
    public class RecyclableMaterials
    {
        [Key]
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public string Material { get; set; }
        [Required]
        public int CategoryId { get; set; }

        // Could potentially add notes here
    }
}
