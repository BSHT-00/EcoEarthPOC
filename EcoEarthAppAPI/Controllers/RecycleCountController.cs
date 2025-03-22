using EcoEarthAppAPI.Data;
using EcoEarthAppAPI.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("{userId}")]
        public async Task<RecycleCategoriesDTO> GetRecycleCount(int userId)
        {
            var recycleCount = await _context.PastRecycledClassCount
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (recycleCount != null)
                return new RecycleCategoriesDTO
                {
                    Cat1 = recycleCount.Cat1,
                    Cat2 = recycleCount.Cat2,
                    Cat3 = recycleCount.Cat3,
                    Cat4 = recycleCount.Cat4,
                    Cat5 = recycleCount.Cat5
                };
            else
                return new RecycleCategoriesDTO
                {
                    Cat1 = -1,
                    Cat2 = -1,
                    Cat3 = -1,
                    Cat4 = -1,
                    Cat5 = -1
                };
        }

        // Increments value of a category count for a user using categoryId
        [HttpPost("{userId}/{categoryId}")]
        public void IncrementCategory(int userId, int categoryId)
        {
            var recycleCount = _context.PastRecycledClassCount
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            if (recycleCount == null)
                return;

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
                    return;
            }

            _context.SaveChanges();
        }


    }
}
