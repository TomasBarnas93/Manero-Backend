using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Dtos.Authentication;

namespace Manero_Backend.Helpers.Factory;

public class IdentityFactory
{
    public static AppUser Create(RegisterForm registrationForm)
    {
        return new()
        {
            FirstName = registrationForm.FirstName,
            LastName = registrationForm.LastName,
            Email = registrationForm.Email,
            UserName = registrationForm.Email
        };
    }
}