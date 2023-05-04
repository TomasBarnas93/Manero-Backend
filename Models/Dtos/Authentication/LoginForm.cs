using System.ComponentModel.DataAnnotations;

namespace Manero_Backend.Models.Dtos.Authentication;

public class LoginForm
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }   = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}