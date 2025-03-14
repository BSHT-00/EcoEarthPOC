using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.Tables
{
    // Composite table which holds info on recycled categories and userid (Shows how many times a user has recycled a certain category)
    public class PastRecycledClassCount
    {
        [Key]
        public int UserId { get; set; }

        [Range(0, int.MaxValue)]
        public int Cat1 { get; set; }
        
        [Range(0, int.MaxValue)]
        public int Cat2 { get; set; }

        [Range(0, int.MaxValue)]
        public int Cat3 { get; set; }

        [Range(0, int.MaxValue)]
        public int Cat4 { get; set; }

        [Range(0, int.MaxValue)]
        public int Cat5 { get; set; }

        public UserProfile userProfile { get; set; }
    }
}
