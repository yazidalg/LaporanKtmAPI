using Microsoft.AspNetCore.Mvc;
using lapora_ktm_api.Services.ReportService;
using lapora_ktm_api.Dtos;
using lapora_ktm_api.Dtos.Response;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lapora_ktm_api.Controllers
{
    // Define the controller as an API controller with a route pattern
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        // Constructor to inject the report service
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // Endpoint to get all reports
        // This method returns a list of reports wrapped in a DefaultResponse object
        // It calls the GetAllReportsAsync method from the report service asynchronously
        [HttpGet]
        public async Task<ActionResult<DefaultResponse<IEnumerable<ReportResponseRelation>>>> GetAllReports()
        {
            var response = await _reportService.GetAllReportsAsync();
            return StatusCode(response.StatusCode, response);
        }

        // Endpoint to get a report by ID
        // This method returns a single report identified by the ID, wrapped in a DefaultResponse object
        // It calls the GetReportByIdAsync method from the report service asynchronously
        [HttpGet("{id}")]
        public async Task<ActionResult<DefaultResponse<ReportDto>>> GetReportById(string id)
        {
            var response = await _reportService.GetReportByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Endpoint to create a new report
        // This method accepts a ReportResponse object and returns a DefaultResponse object
        // It calls the CreateReportAsync method from the report service asynchronously
        [HttpPost]
        public async Task<ActionResult<DefaultResponse<ReportResponse>>> CreateReport(ReportResponse reportDto)
        {
            var response = await _reportService.CreateReportAsync(reportDto);
            return StatusCode(response.StatusCode, response);
        }

        // Endpoint to update an existing report
        // This method accepts a report ID and a ReportDto object, and returns a DefaultResponse object
        // It calls the UpdateReportAsync method from the report service asynchronously
        [HttpPut("{id}")]
        public async Task<ActionResult<DefaultResponse<bool>>> UpdateReport(string id, ReportDto reportDto)
        {
            var response = await _reportService.UpdateReportAsync(id, reportDto);
            return StatusCode(response.StatusCode, response);
        }

        // Endpoint to delete a report by ID
        // This method accepts a report ID and returns a DefaultResponse object
        // It calls the DeleteReportAsync method from the report service asynchronously
        [HttpDelete("{id}")]
        public async Task<ActionResult<DefaultResponse<bool>>> DeleteReport(string id)
        {
            var response = await _reportService.DeleteReportAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}

