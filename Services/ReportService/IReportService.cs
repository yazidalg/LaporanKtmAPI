﻿using lapora_ktm_api.Entities;
using lapora_ktm_api.Dtos.Response;
using lapora_ktm_api.Dtos;

namespace lapora_ktm_api.Services.ReportService
{
	public interface IReportService
	{
        Task<DefaultResponse<IEnumerable<ReportDto>>> GetAllReportsAsync();
        Task<DefaultResponse<ReportDto>> GetReportByIdAsync(string id);
        Task<DefaultResponse<ReportDto>> CreateReportAsync(ReportDto reportDto);
        Task<DefaultResponse<bool>> UpdateReportAsync(string id, ReportDto reportDto);
        Task<DefaultResponse<bool>> DeleteReportAsync(string id);
    }
}

