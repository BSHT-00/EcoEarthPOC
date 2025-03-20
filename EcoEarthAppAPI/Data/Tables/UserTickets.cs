using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace EcoEarthAppAPI.Data.Tables
{
    public class UserTickets
    {
        [Required]
        public int TicketId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [DefaultValue(false)]
        public bool IsCompleted { get; set; }

        public string? UserEmail { get; set; }

        // Will implement in future
        [DefaultValue(null)]
        public byte[]? Image { get; set; }
    }
}
