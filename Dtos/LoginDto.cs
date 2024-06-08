using System;
using System.ComponentModel.DataAnnotations;
using lapora_ktm_api.Entities;
using lapora_ktm_api.Dtos.Response;

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

