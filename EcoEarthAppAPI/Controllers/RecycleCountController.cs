using EcoEarthAppAPI.Data;
using EcoEarthAppAPI.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoEarthAppAPI.Data.Tables;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/RecycleCount")]
    [ApiController]
    public class RecycleCountController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public RecycleCountController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        // Returns the category count for each category
        // Changed during testing to make response clearer
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecycleCount(int userId)
        {
            var recycleCount = await _context.PastRecycledClassCount
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (recycleCount != null)
                return Ok(new RecycleCategoriesDTO
                {
                    Cat1 = recycleCount.Cat1,
                    Cat2 = recycleCount.Cat2,
                    Cat3 = recycleCount.Cat3,
                    Cat4 = recycleCount.Cat4,
                    Cat5 = recycleCount.Cat5
                });
            else
                return NotFound();
        }

        // Increments value of a category count for a user using categoryId
        // Changed during testing to make returns clearer
        [HttpPut("{userId}/{categoryId}")]
        public IActionResult IncrementCategory(int userId, int categoryId)
        {
            var recycleCount = _context.PastRecycledClassCount
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            if (recycleCount == null)
                return NotFound("Cannot find user record");

            switch (categoryId)
            {
                case 1:
                    recycleCount.Cat1++;
                    break;
                case 2:
                    recycleCount.Cat2++;
                    break;
                case 3:
                    recycleCount.Cat3++;
                    break;
                case 4:
                    recycleCount.Cat4++;
                    break;
                case 5:
                    recycleCount.Cat5++;
                    break;
                default:
                    return NotFound("Category doesn't exist");
            }

            _context.SaveChanges();
            return Ok(recycleCount);
        }

        // Creates a blank record for a user if it does not exist (used when user registers)
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateBlankRecord(int userId)
        {
            try
            {
                if (await _context.PastRecycledClassCount.AnyAsync(x => x.UserId == userId))
                {
                    return BadRequest("Record already exists");
                }

                // Added during testing so that negative userIds are not acccepted
                if (userId <= 0)
                {
                    return BadRequest("UserId must be greater than 0");
                }

                else
                {
                    _context.PastRecycledClassCount.Add(new PastRecycledClassCount
                    {
                        UserId = userId,
                        Cat1 = 0,
                        Cat2 = 0,
                        Cat3 = 0,
                        Cat4 = 0,
                        Cat5 = 0
                    });
                    _context.SaveChanges();
                    return Ok("New record created successfully");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
