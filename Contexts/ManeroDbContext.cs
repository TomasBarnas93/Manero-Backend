using Manero_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Contexts
{
	public class ManeroDbContext : DbContext
	{
		public ManeroDbContext(DbContextOptions<ManeroDbContext> options) : base(options)
		{
		}

		DbSet<ProductEntity> Products { get; set; }
		DbSet<CategoryEntity> Category { get; set; }
		DbSet<TagEntity> Tags { get; set; }
	}
}
