namespace Manero_Backend.Models.Schemas.Authentication
{
    public class ChangePasswordSchema
    {
        public string Password { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
