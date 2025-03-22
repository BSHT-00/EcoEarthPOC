using EcoEarthAppAPI.Data;
using Microsoft.AspNetCore.Mvc;
using EcoEarthAppAPI.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/UserTickets")]
    [ApiController]
    public class UserTicketsController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public UserTicketsController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // Gets all tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTickets>>> GetUserTickets()
        {
            return await _context.UserTickets.ToListAsync();
        }

        // Gets a ticket by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTickets>> GetUserTickets(int id)
        {
            var userTickets = await _context.UserTickets.FindAsync(id);
            if (userTickets == null)
            {
                return NotFound();
            }
            return userTickets;
        }

        // Gets incomplete tickets
        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<UserTickets>>> GetIncompleteTickets()
        {
            return await _context.UserTickets
                .Where(x => x.IsCompleted == false)
                .ToListAsync();
        }

        // Marks ticket as complete
        [HttpPut("{ticketId}")]
        public void MarkTicketComplete(int ticketId)
        {
            var userTicket = _context.UserTickets
                .Where(x => x.TicketId == ticketId)
                .Where(x => x.IsCompleted == false)
                .FirstOrDefault();
            if (userTicket == null)
                return;
            userTicket.IsCompleted = true;
            _context.SaveChanges();
        }

        // Posts a new ticket
        [HttpPost]
        public async Task<ActionResult<UserTickets>> PostUserTickets(UserTickets userTickets)
        {
            _context.UserTickets.Add(userTickets);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserTickets", new { id = userTickets.TicketId }, userTickets);
        }
    }
}
