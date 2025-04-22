using System.ComponentModel.DataAnnotations;
using EcoEarthAppAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EcoEarthAppAPI.Data.Tables
{
    //composite table Assigns and holds quests to a user
    public class DailyQuests
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int QuestId { get; set; }

        public int Progress { get; set; }

        public bool isDone { get; set; }

        //one to one
        public UserProfile userProfile { get; set; }

        //1-1
        public Quest Quest { get; set; }

    }
}
