

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using EcoEarthAppAPI.Data.Tables;
using EcoEarthAppAPI.Data;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRewardsController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public UserRewardsController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // POST: api/userrewards/redeem/5
        [HttpPost("redeem/{rewardId}")]
        public async Task<ActionResult> RedeemReward(int rewardId)
        {
            // Normally get this from the authenticated user
            var userId = ""; // real user ID

            var reward = await _context.Rewards.FindAsync(rewardId);
            if (reward == null)
            {
                return NotFound("Reward not found.");
            }

            // Check if the user has enough points before redeeming

            var userReward = new UserReward
            {
                UserId = userId,
                RewardId = reward.RewardId,
                RedeemedAt = DateTime.UtcNow
            };

            _context.UserRewards.Add(userReward);
            await _context.SaveChangesAsync();

            return Ok("Reward redeemed successfully.");
        }

        // GET: api/userrewards/my-rewards
        [HttpGet("my-rewards")]
        public async Task<ActionResult<IEnumerable<UserReward>>> GetMyRewards()
        {
            var userId = ""; //  real authenticated user

            var rewards = await _context.UserRewards
                .Include(ur => ur.Reward)
                .Where(ur => ur.UserId == userId)
                .ToListAsync();

            return rewards;
        }
    }
}
