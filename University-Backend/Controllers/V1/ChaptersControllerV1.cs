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
    public class ChaptersController : ControllerBase
    {
        private readonly UniversityContext _context;

        public ChaptersController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/ChaptersV1
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult<IEnumerable<Chapter>>> GetChapters()
        {
          if (_context.Chapters == null)
          {
              return NotFound();
          }
            return await _context.Chapters.ToListAsync();
        }

        // GET: api/ChaptersV1/5
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult<Chapter>> GetChapter(int id)
        {
          if (_context.Chapters == null)
          {
              return NotFound();
          }
            var chapter = await _context.Chapters.FindAsync(id);

            if (chapter == null)
            {
                return NotFound();
            }

            return chapter;
        }

        // PUT: api/ChaptersV1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutChapter(int id, Chapter chapter)
        {
            if (id != chapter.Id)
            {
                return BadRequest();
            }

            _context.Entry(chapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(id))
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

        // POST: api/ChaptersV1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Chapter>> PostChapter(Chapter chapter)
        {
          if (_context.Chapters == null)
          {
              return Problem("Entity set 'UniversityContext.Chapters'  is null.");
          }
            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChapter", new { id = chapter.Id }, chapter);
        }

        // DELETE: api/ChaptersV1/5
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            if (_context.Chapters == null)
            {
                return NotFound();
            }
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChapterExists(int id)
        {
            return (_context.Chapters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
