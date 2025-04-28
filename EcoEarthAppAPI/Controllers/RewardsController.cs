using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using EcoEarthAppAPI.Data.Tables;
using EcoEarthAppAPI.Data;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public RewardsController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/rewards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reward>>> GetRewards()
        {
            return await _context.Rewards.ToListAsync();
        }
    }
}

