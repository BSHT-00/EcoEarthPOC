using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoEarthAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using EcoEarthAppAPI.Data.Tables;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/UserProfile")]
    [ApiController]
    public class UserProfileController : Controller
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public UserProfileController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // Creates blank profile for new users
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateBlankRecord(int userId)
        {
            if (await _context.UserProfile.FindAsync(userId) != null)
            {
                return BadRequest("User already exists");
            }
            try
            {
                var userProfile = new UserProfile
                {
                    UserId = userId,
                };

                await _context.UserProfile.AddAsync(userProfile);
                _context.SaveChanges();

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating blank record: " + ex.Message);
            }
        }

        // Gets user profile using Id
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int userId)
        {
            var userProfile = await _context.UserProfile.FindAsync(userId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return userProfile;
        }


    }
}
