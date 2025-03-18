using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.Tables
{
    //Holds quest details 
    public class Quest
    {
        [Key]
        [Required]
        public int QuestId { get; set; }

        [Required]
        public string QuestType { get; set; }

        [Required]
        public string QuestDescription {  get; set; }

        [Required]
        public int QuestGoal {  get; set; }

        [Required]
        public int QuestReward { get; set; }
    }
}
