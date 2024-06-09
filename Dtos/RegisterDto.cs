using System;
using System.ComponentModel.DataAnnotations;

namespace lapora_ktm_api.Dtos
{
	public class RegisterDto
	{
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string EmailSSO { get; set; }

        [Required]
        public string Campus { get; set; }

        [Required]
        public string Nim { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

