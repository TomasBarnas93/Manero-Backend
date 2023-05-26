namespace Manero_Backend.Models.Schemas.User
{
    public class ProfilePutSchema
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }
        public string? ImageUrl { get; set; }
    }
}
