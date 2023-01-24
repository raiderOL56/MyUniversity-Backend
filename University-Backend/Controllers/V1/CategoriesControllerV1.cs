using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using University_Backend.Data;
using University_Backend.Models.Data;

namespace University_Backend.Controllers_V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly UniversityContext _context;

        public CategoriesController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/CategoriesV1
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories.ToListAsync();
        }

        // GET: api/CategoriesV1/5
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/CategoriesV1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CategoriesV1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'UniversityContext.Categories'  is null.");
          }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/CategoriesV1/5
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
