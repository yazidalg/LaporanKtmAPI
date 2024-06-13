using lapora_ktm_api.Dtos.Response;
using Microsoft.AspNetCore.Identity;
using lapora_ktm_api.Entities;
using lapora_ktm_api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace lapora_ktm_api.Services.AuthService
{
    // Using Visitor Design Pattern
    // This AuthService for handle authentication logic that will use in AuthController
    public class AuthService : IAuthService
    {

        // Using SignInManager plugin for authentication
        private readonly SignInManager<Student> _signInManager;

        // Using SignInManager plugin for authentication
        private readonly UserManager<Student> _userManager;

        // Constructor for SignInManager and UserManager
        public AuthService(SignInManager<Student> signInManager, UserManager<Student> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Handle login logic for AuthController
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
                        
            if (users is not null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                // Successful login
                return new DefaultResponse<LoginResponse>()
                {
                    StatusCode = 200, // OK
                    Message = "Login Success",
                    Data = new LoginResponse() { Data = user }
                };
            }

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

            return new DefaultResponse<LoginResponse>()
            {
                Data = new LoginResponse() { },
                Message = "Unknown Error",
                StatusCode = 401, // Unauthorized
            };
        }

        // Handle logic Register for AuthController
        public async Task<DefaultResponse<IdentityResult>> RegisterStudent(RegisterDto register)
        {
            // Build an object student fro create user
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

            // Result variable will use in data response
            var result = await _userManager.CreateAsync(student, register.Password);

            // Will return a response from IdentityResult based on Default Response
            return new DefaultResponse<IdentityResult>()
            {
                Data = result,
                Message = result.Succeeded ? "Success Registration" : "Registration Failed",
                StatusCode = result.Succeeded ? 201 : 401,
            };
        }
    }
}

