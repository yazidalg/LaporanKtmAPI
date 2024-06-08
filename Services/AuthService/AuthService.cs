using System;
using lapora_ktm_api.Dtos.Response;
using Microsoft.AspNetCore.Identity;
using lapora_ktm_api.Entities;
using lapora_ktm_api.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace lapora_ktm_api.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<Student> _signInManager;
        private readonly UserManager<Student> _userManager;
        public AuthService(SignInManager<Student> signInManager, UserManager<Student> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<DefaultResponse<LoginResponse>> LoginStudent(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.EmailSSO);
            var result = await _signInManager.PasswordSignInAsync(user.Name, login.Password, false, true);

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

            return new DefaultResponse<LoginResponse>()
            {
                Message = "Login Success",
                Data = new() { Data = user }
            };
        }

        public async Task<DefaultResponse<IdentityResult>> RegisterStudent(RegisterDto register)
        {
            Student student = new()
            {
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

        public string GenerateJWTToken(Student user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}

