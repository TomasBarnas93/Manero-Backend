using Manero_Backend.Contexts;
using Manero_Backend.Models.Entities;

namespace Manero_Backend.Repositories
{
	public class ProductRepository : BaseRepository<ProductEntity>
	{
		public ProductRepository(ManeroDbContext context) : base(context)
		{
		}
	}
}
