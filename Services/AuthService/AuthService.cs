using System;
using lapora_ktm_api.Dtos.Response;
using Microsoft.AspNetCore.Identity;
using lapora_ktm_api.Entities;
using lapora_ktm_api.Dtos;
using lapora_ktm_api.Config;

namespace lapora_ktm_api.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<Student> _signInManager;
        private readonly UserManager<Student> _userManager;
        private Jwt _jwt;

        public AuthService(SignInManager<Student> signInManager, UserManager<Student> userManager, Jwt jwt)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwt = jwt;
        }

        public async Task<DefaultResponse<LoginResponse>> LoginStudent(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.EmailSSO);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, false, true);

            if (user is null)
            {
                return new()
                {
                    Data = new LoginResponse() { },
                    Message = "Email or Password incorrect",
                    StatusCode = 401,
                };
            }

            if (!result.Succeeded)
            {
                return new()
                {
                    Data = new LoginResponse() { },
                    Message = "Email or Password incorrect",
                    StatusCode = 401,
                };
            }

            var token = _jwt.GenerateJWTToken(user);
            return new DefaultResponse<LoginResponse>()
            {
                Message = "Login Success",
                Data = new() { Data = user, Token = token }
                
            };
        }

        public async Task<DefaultResponse<IdentityResult>> RegisterStudent(RegisterDto register)
        {
            Student student = new()
            {
                UserName = register.UserName,
                Name = register.Name,
                Nim = register.Nim,
                EmailSSO = register.EmailSSO,
                Campus = register.Campus,
                Password = register.Password,
            };

            var result = await _userManager.CreateAsync(student, register.Password);

            return new DefaultResponse<IdentityResult>()
            {
                Data = result,
                Message = result.Succeeded ? "Success Registration" : "Registration Failed",
                StatusCode = result.Succeeded ? 201 : 401,
            };
        }
    }
}

