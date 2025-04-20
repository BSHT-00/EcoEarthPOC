using EcoEarthAppAPI.Data.Tables;
using EcoEarthAppAPI.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/DailyStreak")]
    [ApiController]
    public class DailyStreakController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public DailyStreakController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // Gets user's totalDailyStreak by userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<int>> GetDailyStreak(int userId)
        {
            var totalStreak = await _context.DailyStreak.FindAsync(userId);
            if (totalStreak == null)
            {
                return NotFound();
            }
            return totalStreak.TotalStreak;
        }

        //Increments user's total Streak
        [HttpPut("{userId}")]
        public async Task<IActionResult> IncrementStreak(int userId)
        {
            var user = await _context.DailyStreak.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.TotalStreak++;
            await _context.SaveChangesAsync();
            return Ok(user.TotalStreak);
        }

        // Removes daily streak
        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveStreak(int userId)
        {
            var user = await _context.DailyStreak.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.TotalStreak = 0;
            await _context.SaveChangesAsync();
            return Ok(user.TotalStreak);
        }

        //gets last scan date
        [HttpGet("{userId}/LastScanDate")]
        public async Task<ActionResult<DateTime>> GetScanDate(int userId)
        {
            var lastScanDate = await _context.DailyStreak.FindAsync(userId);
            if (lastScanDate == null)
            {
                return NotFound();
            }
            return lastScanDate.LastScanDate;
        }

        //decreases last scan date
        [HttpPut("{userId}/LastScanDate")]
        public async Task<IActionResult> DecreaseScannedDate(int userId)
        {
            var user = await _context.DailyStreak.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.LastScanDate = user.LastScanDate.AddDays(-1);
            await _context.SaveChangesAsync();
            return Ok(user.LastScanDate);
        }

        //updates scanDate to current date
        [HttpPut("{userId}/UpdateScanDate")]
        public async Task<IActionResult> UpdateScanDate(int userId)
        {
            var user = await _context.DailyStreak.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.LastScanDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(user.LastScanDate);
        }


        // Creates blank streak under userId 
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateBlankStreak(int userId)
        {
            if (_context.DailyStreak.Find(userId) != null)
            {
                return BadRequest("account under user id already exists");
            }
            else
            {
                var dailyStreak = new DailyStreak
                {
                    UserId = userId,
                    TotalStreak = 0,
                    LastScanDate = DateTime.UtcNow.AddDays(-1),
                };
                _context.DailyStreak.Add(dailyStreak);
                _context.SaveChanges();
                return Ok(userId);
            }
        }
    }
}
