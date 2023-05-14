using Manero_Backend.Contexts;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Repositories
{
    public class WishRepository : BaseRepository<WishEntity>, IWishRepository
    {
        private readonly ManeroDbContext _context;

        public WishRepository(ManeroDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid productId, string userId)
        {
            return await _context.WishList.Where(w => w.ProductId == productId && w.AppUserId == userId).FirstOrDefaultAsync() != null ? true : false;
        }
        
       

    }
}

/*
 
			return await _context.Products
				.Include(x => x.TagProducts).ThenInclude(x => x.Tag)
				.Include(x => x.Category)
				.Include(x => x.ProductColors).ThenInclude(x => x.Color)
				.Include(x => x.ProductSizes).ThenInclude(x => x.Size)
				.Include(x => x.Reviews)
				.Where(x => (x.Category.Id == option.CategoryId && x.TagProducts.Any(a => a.Tag.Id == option.TagId)) || (x.CategoryId == option.CategoryId) || (x.TagProducts.Any(a => a.Tag.Id == option.TagId))).Take(option.Count).ToListAsync();
 
 */