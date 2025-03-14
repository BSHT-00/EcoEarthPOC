using EcoEarthAppAPI.Data.Tables;
using EcoEarthAppAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/UserCurrency")]
    [ApiController]
    public class UserCurrencyController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public UserCurrencyController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // Gets user's balance by userId
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserBalance(int userId)
        {
            var userBalance = await _context.UserCurrency.FindAsync(userId);
            if (userBalance == null)
            {
                return NotFound();
            }
            return Ok(userBalance.Balance);
        }

        // Adds currency to user's balance
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddCurrency(int userId, [FromBody] int currency)
        {
            var userBalance = await _context.UserCurrency.FindAsync(userId);
            if (userBalance == null)
            {
                return NotFound();
            }
            userBalance.Balance += currency;
            await _context.SaveChangesAsync();
            return Ok(userBalance.Balance);
        }

        // Removes currency from user's balance
        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveCurrency(int userId, [FromBody] int currency)
        {
            var userBalance = await _context.UserCurrency.FindAsync(userId);
            if (userBalance == null)
            {
                return NotFound();
            }
            userBalance.Balance -= currency;
            await _context.SaveChangesAsync();
            return Ok(userBalance.Balance);
        }
    }
}
