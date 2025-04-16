using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoEarthAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using EcoEarthAppAPI.Data.Tables;

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

        // Gets all RecyclableMaterials
        [HttpGet]
        public async Task<IActionResult> GetRecyclableMaterials()
        {
            var recyclableMaterials = await _context.RecyclableMaterials.ToListAsync();
            return Ok(recyclableMaterials);
        }

        // Gets specfic RecyclableMaterial by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecyclableMaterials(int id)
        {
            var recyclableMaterials = await _context.RecyclableMaterials.FindAsync(id);
            if (recyclableMaterials == null)
            {
                return NotFound();
            }

            return Ok(recyclableMaterials);
        }

        // Adding new RecyclableMaterial (Will be implemented under admin panel)
        [HttpPost]
        public async Task<IActionResult> PostRecyclableMaterials([FromBody] RecyclableMaterials recyclableMaterials)
        {
            // Added during testing, check documentation for reasoning
            if (recyclableMaterials.CategoryId <= 0)
                return BadRequest("CategoryId must be greater than 0");
            if (await _context.RecyclableMaterials.FindAsync(recyclableMaterials.MaterialId) != null)
                return BadRequest("Item already exists at this Material Id");

            _context.RecyclableMaterials.Add(recyclableMaterials);
            await _context.SaveChangesAsync();
            return Ok(recyclableMaterials);
        }

        // Removing RecyclableMaterial (Will be implemented under admin panel)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecyclableMaterials(int id)
        {
            var recyclableMaterials = await _context.RecyclableMaterials.FindAsync(id);
            if (recyclableMaterials == null)
            {
                return NotFound();
            }
            _context.RecyclableMaterials.Remove(recyclableMaterials);
            await _context.SaveChangesAsync();
            return Ok(recyclableMaterials);
        }
    }
}
 