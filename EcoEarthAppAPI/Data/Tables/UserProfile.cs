using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcoEarthAppAPI.Data.Tables
{
    public class UserProfile
    {
        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public UserCurrency UserCurrency { get; set; }

        [JsonIgnore]
        public PastRecycledClassCount PastRecycledClassCount { get; set; }

        [JsonIgnore]
        public Login Login { get; set; }

        [JsonIgnore]
        public DailyStreak DailyStreak { get; set; }

        [JsonIgnore]
        public List<DailyQuests> DailyQuests { get; set; }
    }
}
