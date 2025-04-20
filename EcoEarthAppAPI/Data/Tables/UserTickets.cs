using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace EcoEarthAppAPI.Data.Tables
{
    public class UserTickets
    {
        public int TicketId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly Date { get; set; }
        public bool IsCompleted { get; set; }
        public required string Platform { get; set; }
        public required string UserEmail { get; set; }

        // Will implement in future
        [DefaultValue(null)]
        public byte[]? Image { get; set; }
    }
}
