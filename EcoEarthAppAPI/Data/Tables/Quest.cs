using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEarthAppAPI.Data.Tables
{
    //Holds quest details 
    public class Quest
    {
        [Key]
        [Required]
        public int QuestId { get; set; }

        [Required]
        public string QuestType { get; set; }   //Major/Minor

        [Required]
        public string QuestInstruction {  get; set; }  //Recycle/Scan

        [Required]
        public int QuestGoal {  get; set; } //3/5

        [Required]
        public int QuestReward { get; set; }    //5/10


        public int CategoryId { get; set; } //1/2/3/4/5

        [ForeignKey("CategoryId")]
        public RecyclableMaterials RecyclableMaterials { get; set; } //plastic/glass/paper/metal/cardboard
    }
}
