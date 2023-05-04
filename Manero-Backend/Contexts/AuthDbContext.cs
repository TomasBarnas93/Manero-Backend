using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Contexts;

public class AuthDbContext : IdentityDbContext<AppUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }
    //Also need to connect reviews to Appuser
    public DbSet<UserFavoriteEntity> Favorites { get; set; } = null!;
}