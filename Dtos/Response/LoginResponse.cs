using System;
using lapora_ktm_api.Entities;

namespace lapora_ktm_api.Dtos.Response
{
	public class LoginResponse
	{
        public Student Data { get; set; }
        public string Token { get; set; }
    }
}

