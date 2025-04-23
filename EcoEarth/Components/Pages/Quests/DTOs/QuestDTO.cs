using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Pages.Quests.DTOs
{
    internal class QuestDTO
    {
        public int QuestId { get; set; }
        public int UserId { get; set; } //to assign quests to a user

        public string QuestType { get; set; }   //Major/Minor

        public string QuestInstruction { get; set; }  //Recycle/Scan

        public int QuestGoal { get; set; } //3/5

        public int QuestReward { get; set; }    //5/10

        public int CategoryId { get; set; } //1/2/3/4/5/6
                                            //plastic/glass/paper/metal/cardboard/[blank]
        public int Progress { get; set; }

        public bool isDone { get; set; }

        public DateTime LastLoginDate { get; set; } //to set daily quests
    }
}
