namespace EcoEarthAppAPI.Data.Tables
{
    public class UserReward
    
    {
        public int UserRId { get; set; }
        public string UserId { get; set; }
        public int RewardId { get; set; }
        public DateTime RedeemedAt { get; set; }

        public Reward Reward { get; set; }
    }
}

