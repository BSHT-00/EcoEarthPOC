using EcoEarthAppAPI.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoEarthAppAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/RecMaterials")]
    [ApiController]
    public class RecyclableMaterialsController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context; // Add this line to define the _context field

        public RecyclableMaterialsController(EcoEarthAppAPIDbContext context) // Add this constructor to initialize the _context field
        {
            _context = context;
        }

        // GET: api/<RecyclableMaterialsController>
        [HttpGet]
        public async Task<IActionResult> GetMaterialsList()
        {
            var materials = await _context.RecyclableMaterials.ToListAsync();
            return Ok(materials);
        }

        // GET api/<RecyclableMaterialsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RecyclableMaterialsController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<RecyclableMaterialsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<RecyclableMaterialsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
