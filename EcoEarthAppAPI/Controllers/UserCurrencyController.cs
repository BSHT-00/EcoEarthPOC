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
        public async Task<ActionResult<int>> GetUserBalance(int userId)
        {
            var userBalance = await _context.UserCurrency.FindAsync(userId);
            if (userBalance == null)
            {
                return NotFound();
            }
            return userBalance.Balance;
        }

        // Adds currency to user's balance
        [HttpPut("{userId}/{currencyToAdd}")]
        public async Task<IActionResult> AddCurrency(int userId, int currencyToAdd)
        {
            var user = await _context.UserCurrency.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.Balance += currencyToAdd;
            await _context.SaveChangesAsync();
            return Ok(user.Balance);
        }

        // Removes currency to user's balance
        [HttpDelete("{userId}/{currencyToAdd}")]
        public async Task<IActionResult> RemoveCurrency(int userId, int currencyToRemove)
        {
            var user = await _context.UserCurrency.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.Balance -= currencyToRemove;
            await _context.SaveChangesAsync();
            return Ok(user.Balance);
        }
    }
}
