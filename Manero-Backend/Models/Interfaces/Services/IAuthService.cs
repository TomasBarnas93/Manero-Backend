using Manero_Backend.Models.Dtos.Authentication;

namespace Manero_Backend.Models.Interfaces.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterForm user);
    
    Task<string> LoginAsync(LoginForm user);
}