namespace Manero_Backend.Models.Schemas.Authentication
{
    public class LoginSchema
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
