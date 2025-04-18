using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.Tables
{
    public class DailyStreak
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TotalStreak { get; set; }

        public DateTime LastScanDate { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}

