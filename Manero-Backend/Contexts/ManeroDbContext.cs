using Manero_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Contexts
{
	public class ManeroDbContext : DbContext
	{
		public ManeroDbContext(DbContextOptions<ManeroDbContext> options) : base(options)
		{
		}

		public DbSet<ProductEntity> Products { get; set; }
		public DbSet<CategoryEntity> Category { get; set; }
		public DbSet<TagEntity> Tags { get; set; }
		public DbSet<ReviewEntity> Reviews { get; set; }
	}
}
