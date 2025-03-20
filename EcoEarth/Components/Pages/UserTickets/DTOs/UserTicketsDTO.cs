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
        public string Title { get; set; }        
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public bool IsCompleted { get; set; }
        public string? UserEmail { get; set; }
    }
}
