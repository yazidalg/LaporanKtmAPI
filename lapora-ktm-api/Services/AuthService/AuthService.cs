using System;
using lapora_ktm_api.Dtos.Response;
using Microsoft.AspNetCore.Identity;
using lapora_ktm_api.Entities;
using lapora_ktm_api.Dtos;
using lapora_ktm_api.Config;
using Microsoft.EntityFrameworkCore;

namespace lapora_ktm_api.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<Student> _signInManager;
        private readonly UserManager<Student> _userManager;

        public AuthService(SignInManager<Student> signInManager, UserManager<Student> userManager, Jwt jwt)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<DefaultResponse<LoginResponse>> LoginStudent(LoginDto login)
        {
            // Retrieve all users with the given email
            var users = await _userManager.Users.Where(u => u.Email == login.EmailSSO).ToListAsync();

            // Check if there are multiple users with the same email
            if (users.Count > 1)
            {
                return new DefaultResponse<LoginResponse>()
                {
                    Data = new LoginResponse() { },
                    Message = "Multiple users found with the same email",
                    StatusCode = 409, // Conflict
                };
            }

            // Check if no user is found
            if (users.Count == 0)
            {
                return new DefaultResponse<LoginResponse>()
                {
                    Data = new LoginResponse() { },
                    Message = "User not found",
                    StatusCode = 401, // Unauthorized
                };
            }

            var user = users.Single();

            // Check if the password is correct
            var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, false, true);

            // Handle failed login attempt
            if (!result.Succeeded)
            {
                return new DefaultResponse<LoginResponse>()
                {
                    Data = new LoginResponse() { },
                    Message = "Email or Password incorrect",
                    StatusCode = 401, // Unauthorized
                };
            }

            // Successful login
            return new DefaultResponse<LoginResponse>()
            {
                StatusCode = 200, // OK
                Message = "Login Success",
                Data = new LoginResponse() { Data = user }
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
                Email = register.EmailSSO,
                Password = register.Password,
                PhoneNumber = register.Phone,
                Phone = register.Phone,
                Faculty = register.Faculty,
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

