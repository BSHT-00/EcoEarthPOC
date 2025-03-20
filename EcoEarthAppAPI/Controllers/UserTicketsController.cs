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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTickets>>> GetUserTickets()
        {
            return await _context.UserTickets.ToListAsync();
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
