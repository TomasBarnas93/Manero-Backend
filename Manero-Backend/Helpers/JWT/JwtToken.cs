using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Manero_Backend.Helpers.JWT;

public class JwtToken : IJwtToken
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;


    public JwtToken(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(AppUser user)
    {
        
        var identity = await GenerateClaimsIdentity(user);
        var expires = DateTime.UtcNow.AddDays(1);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration.GetSection("TokenValidation").GetValue<string>("Issuer")!,
            Audience = _configuration.GetSection("TokenValidation").GetValue<string>("Audience")!,
            Subject = identity,
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration.GetSection("TokenValidation").GetValue<string>("SecretKey")!)),
                SecurityAlgorithms.HmacSha512Signature    
            )
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(securityTokenDescriptor));
    }
    
    public async Task<ClaimsIdentity> GenerateClaimsIdentity(AppUser identityUser)
    {
        var claimsIdentity = new ClaimsIdentity(new []
        {
            new Claim("id", identityUser.Id.ToString()),
            new Claim(ClaimTypes.Name, identityUser.Email!),
            new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(identityUser))[0])
        });

        return claimsIdentity;
    }
    
    public static string GetEmailFromClaim(HttpContext httpContext)
    {
        // Get the JWT bearer token from the authorization header
        var authHeader = httpContext.Request.Headers["Authorization"].ToString();
        var token = authHeader.Substring("Bearer ".Length).Trim();

        // Decode the JWT token
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);

        // Get the email claim from the JWT token
        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

        return emailClaim!;
    }
}