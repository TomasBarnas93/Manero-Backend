namespace Manero_Backend.Models.Dtos.User
{
    public class UserProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set;}
    }
}
