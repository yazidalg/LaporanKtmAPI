using System.ComponentModel.DataAnnotations;

namespace lapora_ktm_api.Dtos
{
	public class LoginDto
	{
		[Required]
		public string EmailSSO { get; set; }

		[Required]
		public string Password { get; set; }  
	}
}

