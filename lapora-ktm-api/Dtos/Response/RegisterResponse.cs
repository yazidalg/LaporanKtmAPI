﻿using lapora_ktm_api.Entities;

namespace lapora_ktm_api.Dtos.Response
{
	public class RegisterResponse
	{
		public string Message { get; set; }
		public Student Data { get; set; }
	}
}

