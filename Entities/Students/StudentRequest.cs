using System.ComponentModel.DataAnnotations;

namespace lapora_ktm_api.Entities.Students
{
	public class StudentRequest
	{
		[Required]
		public string Name { get; set; }

        [Required]
        public string EmailSSO { get; set; }

        [Required]
        public string Campus { get; set; }

        [Required]
        public string Nim { get; set; }

        [Required]
        public string Password { get; set; }
	}
}

