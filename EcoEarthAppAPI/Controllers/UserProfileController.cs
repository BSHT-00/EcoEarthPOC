using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoEarthAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using EcoEarthAppAPI.Data.Tables;
using Microsoft.Extensions.Options;
using EcoEarthAppAPI.Data.DTOs;

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
            // Added during testing to ensure all userIds are > 0
            if (userId <= 0)
            {
                return BadRequest("UserId must be greater than 0");
            }

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

                // During testing, I overlooked that UserProfile requires a userCurrency and pastRecycledClassCount
                // This was causing a circular reference issue when trying to create a new user profile
                // Create related entities with default values
                var userCurrency = new UserCurrency
                {
                    UserId = userId,
                    Balance = 50,
                };
                var pastRecycledClassCount = new PastRecycledClassCount
                {
                    UserId = userId,
                    Cat1 = 0,
                    Cat2 = 0,
                    Cat3 = 0,
                    Cat4 = 0,
                    Cat5 = 0,
                };

                // Adding the new records from other tables
                await _context.UserCurrency.AddAsync(userCurrency);
                await _context.UserProfile.AddAsync(userProfile);
                await _context.PastRecycledClassCount.AddAsync(pastRecycledClassCount);

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
            // Added during testing, this prevents negative userIds from being looked up
            if (userId <= 0)
                return BadRequest("userId cannot be negative");

            var userProfile = await _context.UserProfile.FindAsync(userId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }


    }
}
