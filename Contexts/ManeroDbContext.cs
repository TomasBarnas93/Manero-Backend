using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Contexts
{
	public class ManeroDbContext : DbContext
	{
		public ManeroDbContext(DbContextOptions<DbContext> options) : base(options)
		{
		}
	}
}
