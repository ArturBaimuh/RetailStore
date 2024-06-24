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
    public class MarkupController : ControllerBase
    {
        private readonly StoreContext _context;

        public MarkupController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("GetMarkups")]
        public async Task<ActionResult<IEnumerable<Markup>>> GetMarkups()
        {
            return await _context.Markups.ToListAsync();
        }

        [HttpGet("GetMarkup")]
        public async Task<ActionResult<Markup>> GetMarkup(int id)
        {
            var markup = await _context.Markups.FindAsync(id);

            if (markup == null)
            {
                return NotFound();
            }

            return markup;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Markup>> CreateMarkup([FromBody]Markup markup)
        {
            _context.Markups.Add(markup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarkup", new { id = markup.Id }, markup);
        }

        [HttpPost("UpdateMarkup")]
        public async Task<IActionResult> UpdateMarkup(int id, [FromBody] Markup markup)
        {
            if (id != markup.Id)
            {
                return BadRequest();
            }

            _context.Entry(markup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkupExists(id))
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

        [HttpGet("DeleteMarkup")]
        public async Task<IActionResult> DeleteMarkup(int id)
        {
            var markup = await _context.Markups.FindAsync(id);
            if (markup == null)
            {
                return NotFound();
            }

            _context.Markups.Remove(markup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarkupExists(int id)
        {
            return _context.Markups.Any(e => e.Id == id);
        }
    }
}
