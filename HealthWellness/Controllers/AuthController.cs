using HealthWellness.DTOs;
using HealthWellness.Helpers;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = _authService.Register(dto);
            return Ok(ApiResponse<User>.Ok(user, "User registered successfully"));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var result = _authService.Login(dto);

            if (result == null)
                return Unauthorized(ApiResponse<object>.Fail("Invalid credentials"));

            return Ok(ApiResponse<AuthResponseDto>.Ok(result, "Login successful"));
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var username = User.Identity!.Name!;
            var user = _authService.GetProfile(username);

            return Ok(ApiResponse<User>.Ok(user!, "Profile fetched"));
        }
    }
}
