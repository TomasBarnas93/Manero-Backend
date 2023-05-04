using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Dtos.Authentication;
using Manero_Backend.Models.Interfaces.Services;

namespace Manero_Backend.Helpers.Services;

public class AuthService : IAuthService
{
    public async Task<bool> RegisterAsync(RegisterForm user)
    {
        throw new NotImplementedException();
    }

    public async Task<string> LoginAsync(LoginForm user)
    {
        throw new NotImplementedException();
    }
}