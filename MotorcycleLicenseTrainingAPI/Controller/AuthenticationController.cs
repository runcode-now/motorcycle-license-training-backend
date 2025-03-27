using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Service.Implementation;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginUserAsync(request);
            if (response == null)
            {
                return Unauthorized(new { authenticated = false, message = "Invalid email or password." });
            }

            return Ok(response);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var (isSuccess, message) = await _authService.RegisterUserAsync(request);

            if (!isSuccess)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }
    }
}
