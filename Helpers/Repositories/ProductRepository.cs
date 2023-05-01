using Manero_Backend.Contexts;
using Manero_Backend.Models.Dtos;
using Manero_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Repositories
{
	public class ProductRepository : BaseRepository<ProductEntity>
	{
		private readonly ManeroDbContext _context;

		public ProductRepository(ManeroDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ProductResponse>> GetAllAsync()
		{
			var list = await _context.Products
				.Include(x => x.Category)
				.Include(x => x.Tag)
				.ToListAsync();

			var resultList = new List<ProductResponse>();


			foreach (var item in list)
			{
				resultList.Add(item);
			}
			return resultList;
		}
	}
}
