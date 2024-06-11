using System.ComponentModel.DataAnnotations;

namespace lapora_ktm_api.Dtos
{
	public class ReportDto
	{
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public string StudentId { get; set; }
    }
}

