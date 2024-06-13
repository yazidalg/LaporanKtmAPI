using Microsoft.AspNetCore.Identity;
using lapora_ktm_api.Dtos.Response;
using lapora_ktm_api.Dtos;

namespace lapora_ktm_api.Services.AuthService
{
	// Visitor Design Pattern
	// This interface will use in AuthService class for defining what the AuthService do
	public interface IAuthService
	{
		Task<DefaultResponse<IdentityResult>> RegisterStudent(RegisterDto register);
		Task<DefaultResponse<LoginResponse>> LoginStudent(LoginDto login);
	}
}

