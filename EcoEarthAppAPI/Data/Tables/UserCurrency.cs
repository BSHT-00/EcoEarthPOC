using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEarthAppAPI.Data.Tables
{
    public class UserCurrency
    {
        [Required]
        [Key]
        public int UserId;

        [Required]
        [Range(0, int.MaxValue)]
        public int Balance;

        public UserProfile userProfile;
    }
}
