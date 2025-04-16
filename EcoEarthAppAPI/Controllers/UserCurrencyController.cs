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
        [HttpPut("{userId}/{currency}")]
        public async Task<IActionResult> AddCurrency(int userId, int currency)
        {
            if(currency <= 0)
            {
                return BadRequest("Can't add a negative value");
            }

            var user = await _context.UserCurrency.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.Balance += currency;
            await _context.SaveChangesAsync();
            return Ok(user.Balance);
        }

        // Removes currency to user's balance
        [HttpDelete("{userId}/{currency}")]
        public async Task<IActionResult> RemoveCurrency(int userId, int currency)
        {
            if (currency <= 0)
            {
                return BadRequest("Can't deduct a negative value");
            }

            var user = await _context.UserCurrency.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.Balance -= currency;
            await _context.SaveChangesAsync();
            return Ok(user.Balance);
        }

        // Creates blank record under userId with a starting balance of 50
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateBlankRecord(int userId)
        {
            if (_context.UserCurrency.Find(userId) != null)
            {
                return BadRequest("account under user id already exists");
            }
            else
            {
                var userCurrency = new UserCurrency
                {
                    UserId = userId,
                    Balance = 50,
                };
                _context.UserCurrency.Add(userCurrency);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
