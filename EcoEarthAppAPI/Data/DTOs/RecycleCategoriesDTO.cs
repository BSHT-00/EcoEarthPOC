using System.ComponentModel.DataAnnotations;

namespace EcoEarthAppAPI.Data.DTOs
{
    public class RecycleCategoriesDTO
    {
        // Plastic
        [Range(0, int.MaxValue)]
        public int Cat1 { get; set; }


        // Glass
        [Range(0, int.MaxValue)]
        public int Cat2 { get; set; }


        // Metal
        [Range(0, int.MaxValue)]
        public int Cat3 { get; set; }


        // Paper
        [Range(0, int.MaxValue)]
        public int Cat4 { get; set; }

        // Cardboard
        [Range(0, int.MaxValue)]
        public int Cat5 { get; set; }
    }
}
