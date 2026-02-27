using HealthWellness.DTOs;
using HealthWellness.Models;
using HealthWellness.Interfaces;

namespace HealthWellness.Interfaces
{
    public interface IAuthService
    {
        AuthResponseDto? Login(LoginDto dto);
        User Register(RegisterDto dto);
        User? GetProfile(string username);
    }
}
