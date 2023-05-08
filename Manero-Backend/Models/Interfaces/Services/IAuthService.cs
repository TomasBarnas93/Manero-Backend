using System.Net;
using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Dtos.Authentication;

namespace Manero_Backend.Models.Interfaces.Services;

public interface IAuthService
{
    Task<HttpStatusCode> RegisterAsync(RegisterForm user);
    
    Task<string> LoginAsync(LoginForm user);
    Task<bool> LogoutAsync();
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<AppUser>> GetAllAsync();
}