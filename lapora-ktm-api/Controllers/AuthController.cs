using Microsoft.AspNetCore.Mvc;
using lapora_ktm_api.Services.AuthService;
using lapora_ktm_api.Dtos;

namespace lapora_ktm_api.Controllers
{
    // Define the route for this controller
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Constructor to inject the authentication service
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Endpoint for logging in
        // This method accepts a LoginDto object and returns an IActionResult
        // The LoginDto contains the login details
        // The method uses the injected _authService to handle the login process asynchronously
        [HttpPost, Route("login")]
        public async Task<IActionResult> SignIn(LoginDto login) => Ok(await _authService.LoginStudent(login));

        // Endpoint for registering a new user
        // This method accepts a RegisterDto object and returns an IActionResult
        // The RegisterDto contains the registration details
        // The method uses the injected _authService to handle the registration process asynchronously
        [HttpPost, Route("register")]
        public async Task<IActionResult> SignUp(RegisterDto register) => Ok(await _authService.RegisterStudent(register));
    }
}

