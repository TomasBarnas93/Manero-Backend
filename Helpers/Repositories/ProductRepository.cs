using Manero_Backend.Contexts;
using Manero_Backend.Helpers.Repositories;
using Manero_Backend.Models.Dtos;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Repositories
{
	public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
	{
		private readonly ManeroDbContext _context;

		public ProductRepository(ManeroDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
