using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoEarthAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using EcoEarthAppAPI.Data.Tables;


namespace EcoEarthAppAPI.Controllers
{
    // Contoller used to create new accounts
    [Route("api/InitializeNewAccount")]
    [ApiController]
    public class InitializeNewAccount : Controller
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public InitializeNewAccount(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // Initialize a new account with default values
        [HttpPost]
        public async Task<IActionResult> CreateNewAccount()
        {
            int userId = GetNextId();

            // If account already exists, return bad request
            if (await _context.UserProfile.FindAsync(userId) != null)
            {
                return BadRequest("User already exists");
            }

            try
            {
                // Getting next Id to be occupied
                int newUserId = GetNextId();

                // Creating blank records
                UserCurrencyController userCurrency = new UserCurrencyController(_context);
                await userCurrency.CreateBlankRecord(newUserId);

                UserProfileController userProfile = new UserProfileController(_context);
                await userProfile.CreateBlankRecord(newUserId);

                RecycleCountController recycleCount = new RecycleCountController(_context);
                await recycleCount.CreateBlankRecord(newUserId);

                return Ok(userId);
            }
            catch
            {
                return BadRequest("Error creating new account");
            }
        }

        // Returns the next UserId to be used
        [HttpGet]
        public int GetNextId()
        {
            // List to hold the last userId from each table, will be sorted to return largest value.
            List<int> userIds = new List<int>();

            // Getting the last userId from each table, default value is set to 1
            var lastUserProfile = _context.UserProfile.OrderByDescending(u => u.UserId).FirstOrDefault()?.UserId ?? 1;
            var lastUserCurr = _context.UserCurrency.OrderByDescending(u => u.UserId).FirstOrDefault()?.UserId ?? 1;
            var lastUserCount = _context.PastRecycledClassCount.OrderByDescending(u => u.UserId).FirstOrDefault()?.UserId ?? 1;

            // Adding the values to the list (to sort)
            userIds.Add(lastUserProfile);
            userIds.Add(lastUserCurr);
            userIds.Add(lastUserCount);

            // Returns the largest value in the list
            return userIds.Max() + 1; 
        }


    }
}
