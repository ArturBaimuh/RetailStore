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
    public class ShopController : ControllerBase
    {
        private readonly StoreContext _context;

        public ShopController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("GetShops")]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShops()
        {
            return await _context.Shops.Include(s => s.Stock).ToListAsync();
        }

        [HttpGet("GetShop")]
        public async Task<ActionResult<Shop>> GetShop(int id)
        {
            var shop = await _context.Shops.Include(s => s.Stock).FirstOrDefaultAsync(s => s.Id == id);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        [HttpPost("CreateShop")]
        public async Task<ActionResult<Shop>> CreateShop([FromBody] Shop shop)
        {
            _context.Shops.Add(shop);
            shop.Stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == shop.StockId);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShop", new { id = shop.Id }, shop);
        }

        [HttpPut("UpdateShop/{id}")]
        public async Task<IActionResult> UpdateShop(int id, [FromBody] Shop shop)
        {
            if (id != shop.Id)
            {
                return BadRequest();
            }

            _context.Entry(shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        [HttpDelete("DeleteShop/{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.Id == id);
        }
    }
}