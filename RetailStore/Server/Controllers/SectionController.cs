using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReatailStore.Domain.Entities;
using ReatailStore.Domain.StoreDbContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly StoreContext _context;

        public SectionController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("GetSections")]
        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            return await _context.Sections.ToListAsync();
        }

        [HttpGet("GetSection")]
        public async Task<ActionResult<Section>> GetSection(int id)
        {
            var section = await _context.Sections.Include(s => s.Markup).FirstOrDefaultAsync(s => s.Id == id);

            if (section == null)
            {
                return NotFound();
            }

            return section;
        }

        [HttpPost("CreateSection")]
        public async Task<ActionResult<Section>> CreateSection([FromBody] Section section)
        {
            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSection", new { id = section.Id }, section);
        }

        [HttpPost("UpdateSection")]
        public async Task<IActionResult> UpdateSection(int id, Section section)
        {
            if (id != section.Id)
            {
                return BadRequest();
            }

            _context.Entry(section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
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

        [HttpGet("DeleteSection")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
