using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.Tables
{
    //composite table Assigns and holds quests to a user
    public class DailyQuests
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int QuestId { get; set; }

        public int Progress { get; set; }

        public bool isDone { get; set; }

        //one to one
        public UserProfile userProfile { get; set; }

        //one to many
        public List<Quest>? Quests { get; set; }

    }
}
