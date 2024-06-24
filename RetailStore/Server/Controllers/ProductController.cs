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
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.Include(p => p.Provider)
                .ToListAsync();
        }
        [HttpGet("GetProductsForStock")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int idStock)
        {
            return await _context.Products.Include(p => p.Provider).Include(p => p.Stock).Where(p => p.StockId == idStock)
                .ToListAsync();
        }


        [HttpGet("GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        [HttpGet("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [HttpGet("GetProductsByName")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name, int id)
        {
            var products = await _context.Products
                .Include(p => p.Provider)//.Include(p => p.Stock)
                .Where(p => p.Name.Contains(name) && p.StockId == id)
                .ToListAsync();

            return products;
        }

        [HttpGet("GetProductsByBarcode")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByBarcode(string barcode, int id)
        {
            var products = await _context.Products
                .Include(p => p.Provider)//.Include(p => p.Stock)
                .Where(p => p.Barcode == barcode && p.StockId == id)
                .ToListAsync();

            return products;
        }
    }
}
