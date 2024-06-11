using Microsoft.AspNetCore.Mvc;
using lapora_ktm_api.Services.AuthService;
using lapora_ktm_api.Dtos;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lapora_ktm_api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> SignIn(LoginDto login) => Ok(await _authService.LoginStudent(login));

        [HttpPost, Route("register")]
        public async Task<IActionResult> SignUp(RegisterDto register) => Ok(await _authService.RegisterStudent(register));
    }
}

