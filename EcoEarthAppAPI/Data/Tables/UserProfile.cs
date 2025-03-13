using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.Tables
{
    public class UserProfile
    {
        [Required]
        [Key]
        public int UserId { get; set; }

        public UserCurrency userCurrency { get; set; }
        public PastRecycledClassCount pastRecycledClassCount { get; set; }
    }
}
