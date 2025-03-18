using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.Tables
{
    public class UserProfile
    {
        [Required]
        [Key]
        public int UserId { get; set; }

        public UserCurrency UserCurrency { get; set; }
        public PastRecycledClassCount PastRecycledClassCount { get; set; }
    }
}
