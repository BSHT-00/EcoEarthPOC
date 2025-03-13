using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoEarthAppAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/RecyclableMaterials")]
    [ApiController]
    public class RecyclableMaterialsController : Controller
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public RecyclableMaterialsController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecyclableMaterials()
        {
            var recyclableMaterials = await _context.RecyclableMaterials.ToListAsync();
            return Ok(recyclableMaterials);
        }
    }
}
