using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lapora_ktm_api.Entities;
using lapora_ktm_api.Services.ReportService;
using lapora_ktm_api.Dtos;
using lapora_ktm_api.Dtos.Response;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lapora_ktm_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ReportResponseRelation>>>> GetAllReports()
        {
            var response = await _reportService.GetAllReportsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DefaultResponse<ReportDto>>> GetReportById(string id)
        {
            var response = await _reportService.GetReportByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<DefaultResponse<ReportResponse>>> CreateReport(ReportResponse reportDto)
        {
            var response = await _reportService.CreateReportAsync(reportDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DefaultResponse<bool>>> UpdateReport(string id, ReportDto reportDto)
        {
            var response = await _reportService.UpdateReportAsync(id, reportDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DefaultResponse<bool>>> DeleteReport(string id)
        {
            var response = await _reportService.DeleteReportAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}

