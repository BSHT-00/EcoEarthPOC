using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.Tables
{
    public class UserCurrency
    {
        [Required]
        [Key]
        public int UserId;

        [Required]
        [Range(0, int.MaxValue)]
        public int Currency;

        public UserProfile userProfile;
    }
}
