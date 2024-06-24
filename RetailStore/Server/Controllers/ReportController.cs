using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReatailStore.Domain.Entities;
using ReatailStore.Domain.StoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly StoreContext _context;

        public ReportController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("GetReports")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _context.Reports.Include(r => r.ReportItems).ThenInclude(r=>r.Product).ToListAsync();
        }

        [HttpGet("GetReport")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _context.Reports.Include(r => r.ReportItems).FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [HttpPost("CreateReport")]
        public async Task<ActionResult<Report>> CreateReport([FromBody] Report report)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Добавляем отчет в контекст
                    _context.Reports.Add(report);

                    // Обновляем количество продуктов и сохраняем изменения в базе данных
                    foreach (var reportItem in report.ReportItems)
                    {
                        //var product = await _context.Products.AsNoTracking().Include(p => p.Stock)
                        //    .Include(p => p.Provider).Include(p => p.Section)
                        //    .FirstOrDefaultAsync(p => p.Id == reportItem.ProductId);//FindAsync(reportItem.ProductId);
                        //product.Stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == product.StockId);
                        if (reportItem.Product != null)
                        {
                            if (reportItem.isAmount)
                            {
                                reportItem.Product.Amount -= reportItem.Amount;
                            }
                            else if (reportItem.isWight)
                            {
                                // Обработка уменьшения веса продукта
                            }
                            _context.Entry(reportItem.Product).State = EntityState.Modified;
                        }
                    }

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return CreatedAtAction("GetReport", new { id = report.Id }, report);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return StatusCode(500, "Ошибка сервера при создании отчета.");
                }
            }
        }

        [HttpPost("UpdateReport")]
        public async Task<IActionResult> UpdateReport(int id, Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
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

        [HttpGet("DeleteReport")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
