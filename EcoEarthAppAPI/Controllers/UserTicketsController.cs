using EcoEarthAppAPI.Data;
using Microsoft.AspNetCore.Mvc;
using EcoEarthAppAPI.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.RegularExpressions;

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
            return Ok(await _context.UserTickets.ToListAsync());
        }

        // Gets a ticket by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTickets>> GetUserTickets(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ticket ID");
            }

            var userTickets = await _context.UserTickets.FindAsync(id);
            if (userTickets == null)
            {
                return NotFound("There is no ticket with that id");
            }
            return Ok(userTickets);
        }

        // Gets incomplete tickets
        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<UserTickets>>> GetIncompleteTickets()
        {
            // OrderBy added in during testing
            return Ok(await _context.UserTickets
                .Where(x => x.IsCompleted == false)
                .OrderBy(a => a.Date)
                .ToListAsync());
        }

        // Marks ticket as complete
        [HttpPut("{ticketId}")]
        public IActionResult MarkTicketComplete(int ticketId)
        {
            if (ticketId <= 0)
            {
                return BadRequest("Invalid ticket ID");
            }

            var userTicket = _context.UserTickets
                .Where(x => x.TicketId == ticketId)
                .Where(x => x.IsCompleted == false)
                .FirstOrDefault();

            if (userTicket == null)
                return NotFound();

            userTicket.IsCompleted = true;
            _context.SaveChanges();
            return Ok(userTicket);
        }

        // Posts a new ticket
        [HttpPost]
        public async Task<ActionResult<UserTickets>> PostUserTickets(UserTickets userTickets)
        {
            // Validation Added During Testing
            if (userTickets.Title.Length < 10 )
                return BadRequest("Title must be more than 10 characters");
            if (userTickets.Description.Length < 40)
                return BadRequest("Description must be more than 40 characters");

            // Checks Email (if provided) is in valid format using a reg expression found here: https://mailtrap.io/blog/validate-email-address-c/
            if (userTickets.UserEmail != null)
            {
                Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$");
                var validation = regex.Match(userTickets.UserEmail);

                if (!validation.Success)
                    return BadRequest("Email is not valid");
            }


            _context.UserTickets.Add(userTickets);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserTickets", new { id = userTickets.TicketId }, userTickets);
        }
    }
}
