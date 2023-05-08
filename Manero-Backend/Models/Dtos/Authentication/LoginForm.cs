namespace Manero_Backend.Models.Dtos.Authentication;

public class LoginForm
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}