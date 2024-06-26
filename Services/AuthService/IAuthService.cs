﻿using Microsoft.AspNetCore.Identity;
using lapora_ktm_api.Dtos.Response;
using lapora_ktm_api.Dtos;

namespace lapora_ktm_api.Services.AuthService
{
	public interface IAuthService
	{
		Task<DefaultResponse<IdentityResult>> RegisterStudent(RegisterDto register);
		Task<DefaultResponse<LoginResponse>> LoginStudent(LoginDto login);
	}
}

