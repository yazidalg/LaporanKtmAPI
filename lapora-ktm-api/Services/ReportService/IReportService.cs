using lapora_ktm_api.Dtos.Response;
using lapora_ktm_api.Dtos;

namespace lapora_ktm_api.Services.ReportService
{

    // Visitor Design Pattern
    // This interface will tell what the ReportService class do.
	public interface IReportService
	{
        Task<DefaultResponse<IEnumerable<ReportResponseRelation>>> GetAllReportsAsync();
        Task<DefaultResponse<ReportDto>> GetReportByIdAsync(string id);
        Task<DefaultResponse<ReportResponse>> CreateReportAsync(ReportResponse reportDto);
        Task<DefaultResponse<bool>> UpdateReportAsync(string id, ReportDto reportDto);
        Task<DefaultResponse<bool>> DeleteReportAsync(string id);
    }
}

