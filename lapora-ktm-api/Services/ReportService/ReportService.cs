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

        public async Task<DefaultResponse<IEnumerable<ReportResponseRelation>>> GetAllReportsAsync()
        {
            var reports = await _dbContext.Reports.ToListAsync();
            IQueryable<Report> query = _dbContext.Reports;

            var reportDtos = query.Select(r => new ReportResponseRelation
            {
                Id = r.Id,
                CreatedAt = r.CreatedAt,
                Description = r.Description,
                Title = r.Title,
                Status = r.Status,
                StudentId = r.StudentId,
                Student = new () {
                    Id = r.Student.Id,
                    Email = r.Student.Email,
                    Nim = r.Student.Nim,
                    Name = r.Student.Name,
                    Phone = r.Student.Phone,
                    PhoneNumber = r.Student.PhoneNumber,
                    UserName = r.Student.UserName,
                    Faculty = r.Student.Faculty,
                    EmailSSO = r.Student.EmailSSO,
                    Password = r.Student.Password,
                },
            });

            return new DefaultResponse<IEnumerable<ReportResponseRelation>>
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

        public async Task<DefaultResponse<ReportResponse>> CreateReportAsync(ReportResponse reportDto)
        {

            var report = new Report
            {
                Id = Guid.NewGuid().ToString(),
                Title = reportDto.Title,
                Description = reportDto.Description,
                Status = reportDto.Status,
                CreatedAt = reportDto.CreatedAt,
                StudentId = reportDto.StudentId,
            };

            _dbContext.Reports.Add(report);
            await _dbContext.SaveChangesAsync();

            reportDto.Id = report.Id;

            return new DefaultResponse<ReportResponse>
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

