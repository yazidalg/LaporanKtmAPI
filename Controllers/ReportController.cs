using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lapora_ktm_api.Entities;
using lapora_ktm_api.Config;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lapora_ktm_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ReportController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllReport()
        {
            try
            {
                var reports = await _dbContext.Reports.ToListAsync();
                return Ok(reports);
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReportById(int id)
        {
            var report = await _dbContext.Reports.FindAsync(id);

            try
            {
                if (report is null)
                {
                    return NotFound();
                }

                return Ok(report);
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Report>> CreateReport(Report report)
        {
            try
            {
                _dbContext.Reports.Add(report);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAllReport), new { id = report.Id }, report);
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(report).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!_dbContext.Reports.Any(e => e.Id == id)) {
                    return NotFound();
                } else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _dbContext.Reports.FindAsync(id);

            if (report is null)
            {
                return NotFound();
            }

            _dbContext.Reports.Remove(report);
            _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

