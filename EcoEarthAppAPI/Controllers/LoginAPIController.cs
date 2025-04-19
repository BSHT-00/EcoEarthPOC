using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoEarthAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using EcoEarthAppAPI.Data.Tables;


namespace EcoEarthAppAPI.Controllers
{
    // Contoller used to create new accounts
    [Route("api/login")]
    [ApiController]
    public class LoginAPIController : Controller
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public LoginAPIController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // https://localhost:7111/api/login?loginId=loginId
        // Initialize a new account with default values
        [HttpPost]
        public async Task<IActionResult> CreateNewAccount(string loginId)
        {
            // Get next id in EEAPI
            int newUserId = GetNextId();

            // If account already exists, return bad request
            if (await _context.UserProfile.FindAsync(newUserId) != null)
            {
                return BadRequest("User already exists");
            }

            try
            {
                // Creating login table record
                await RegisterUser(newUserId, loginId);

                // Creating blank records
                // All blank tables are created inside the UserProfileController
                UserProfileController userProfile = new UserProfileController(_context);
                await userProfile.CreateBlankRecord(newUserId);

                // returns the new userId and the user can then login
                return Ok(newUserId);
            }
            catch (Exception e)
            {
                return BadRequest("Error creating new account" + e.Message );
            }
        }

        // Returns the next UserId to be used
        [HttpGet]
        public int GetNextId()
        {
            // List to hold the last userId from each table, will be sorted to return largest value.
            List<int> userIds = new List<int>();

            // Getting the last userId from each table, default value is 1
            var lastUserProfile = _context.UserProfile.OrderByDescending(u => u.UserId).FirstOrDefault()?.UserId ?? 1;
            var lastUserCurr = _context.UserCurrency.OrderByDescending(u => u.UserId).FirstOrDefault()?.UserId ?? 1;
            var lastUserCount = _context.PastRecycledClassCount.OrderByDescending(u => u.UserId).FirstOrDefault()?.UserId ?? 1;
            var lastUserLogin = _context.Login.OrderByDescending(u => u.UserId).FirstOrDefault()?.UserId ?? 1;

            // Adding the values to the list (to sort)
            userIds.Add(lastUserProfile);
            userIds.Add(lastUserCurr);
            userIds.Add(lastUserCount);
            userIds.Add(lastUserLogin);

            // Returns the largest value in the list
            return userIds.Max() + 1; 
        }

        // Creates record in the login table
        [HttpPost("{userId}/{LoginId}")]
        public async Task<IActionResult> RegisterUser(int userId, string LoginId)
        {
            // Check if record already exists
            var existingLogin = await _context.Login
                .FirstOrDefaultAsync(l => l.UserId == userId && l.LoginAPIId == LoginId);

            // If it does exist, return BadRequest
            if (existingLogin != null)
                return BadRequest("User record exists");

            // If it doesn't, create the record
            Login newLogin = new Login
            {
                UserId = userId,
                LoginAPIId = LoginId
            };

            try
            {
                _context.Login.Add(newLogin);
                await _context.SaveChangesAsync();
                return Ok(newLogin.UserId);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (optional)
                return BadRequest(ex.Message);            
            }
        }

        // Returns the userId associated with the loginId, will be used for logging in
        [HttpGet("{loginId}")]
        public async Task<IActionResult> GetUserId(string loginId)
        {
            // Check if record exists
            var existingLogin = await _context.Login
                .FirstOrDefaultAsync(l => l.LoginAPIId == loginId);

            if (existingLogin == null)
                return NotFound("User not found");

            return Ok(existingLogin.UserId);
        }


    }
}
