using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEarthAppAPI.Data.Tables
{
    public class UserCurrency
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Balance { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
