using System;
using lapora_ktm_api.Entities;
using lapora_ktm_api.Config;
using Microsoft.EntityFrameworkCore;
using lapora_ktm_api.Dtos.Response;
using lapora_ktm_api.Dtos;

namespace lapora_ktm_api.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _dbContext;

        public ReportService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DefaultResponse<IEnumerable<ReportDto>>> GetAllReportsAsync()
        {
            var reports = await _dbContext.Reports.ToListAsync();
            var reportDtos = reports.Select(r => new ReportDto
            {
                Title = r.Title,
                Description = r.Description,
                Status = r.Status,
                CreatedAt = r.CreatedAt,
                StudentId = r.StudentId
            });

            return new DefaultResponse<IEnumerable<ReportDto>>
            {
                StatusCode = 200,
                Message = "Success",
                Data = reportDtos
            };
        }

        public async Task<DefaultResponse<ReportDto>> GetReportByIdAsync(string id)
        {
            var report = await _dbContext.Reports.FindAsync(id);

            if (report == null)
            {
                return new DefaultResponse<ReportDto>
                {
                    StatusCode = 404,
                    Message = "Report not found",
                    Data = new ReportDto() { }
                };
            }

            var reportDto = new ReportDto
            {
                Title = report.Title,
                Description = report.Description,
                Status = report.Status,
                CreatedAt = report.CreatedAt,
                StudentId = report.StudentId
            };

            return new DefaultResponse<ReportDto>
            {
                StatusCode = 200,
                Message = "Success",
                Data = reportDto
            };
        }

        public async Task<DefaultResponse<ReportDto>> CreateReportAsync(ReportDto reportDto)
        {
            var report = new Report
            {
                Id = Guid.NewGuid().ToString(),
                Title = reportDto.Title,
                Description = reportDto.Description,
                Status = reportDto.Status,
                CreatedAt = reportDto.CreatedAt,
                StudentId = reportDto.StudentId
            };

            _dbContext.Reports.Add(report);
            await _dbContext.SaveChangesAsync();

            reportDto.Id = report.Id;

            return new DefaultResponse<ReportDto>
            {
                StatusCode = 201,
                Message = "Report created",
                Data = reportDto
            };
        }

        public async Task<DefaultResponse<bool>> UpdateReportAsync(string id, ReportDto reportDto)
        {
            var report = await _dbContext.Reports.FindAsync(id);
            if (report == null)
            {
                return new DefaultResponse<bool>
                {
                    StatusCode = 404,
                    Message = "Report not found",
                    Data = false
                };
            }

            report.Title = reportDto.Title;
            report.Description = reportDto.Description;
            report.Status = reportDto.Status;
            report.CreatedAt = reportDto.CreatedAt;
            report.StudentId = reportDto.StudentId;

            _dbContext.Entry(report).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return new DefaultResponse<bool>
                {
                    StatusCode = 204,
                    Message = "Report updated",
                    Data = true
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Reports.Any(e => e.Id == id))
                {
                    return new DefaultResponse<bool>
                    {
                        StatusCode = 404,
                        Message = "Report not found",
                        Data = false
                    };
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<DefaultResponse<bool>> DeleteReportAsync(string id)
        {
            var report = await _dbContext.Reports.FindAsync(id);

            if (report == null)
            {
                return new DefaultResponse<bool>
                {
                    StatusCode = 404,
                    Message = "Report not found",
                    Data = false
                };
            }

            _dbContext.Reports.Remove(report);
            await _dbContext.SaveChangesAsync();
            return new DefaultResponse<bool>
            {
                StatusCode = 204,
                Message = "Report deleted",
                Data = true
            };
        }
    }
}

