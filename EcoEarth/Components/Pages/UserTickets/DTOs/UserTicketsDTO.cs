using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Pages.UserTickets.DTOs
{
    // Used to post tickets to the API
    public class UserTicketsDTO
    {
        public int TicketId { get; set; }
        public required string Title { get; set; }        
        public required string Description { get; set; }
        public DateOnly Date { get; set; }
        public bool IsCompleted { get; set; }
        public required string Platform { get; set; }
        public required string UserEmail { get; set; }
    }
}
