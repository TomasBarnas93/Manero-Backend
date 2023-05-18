using Manero_Backend.Contexts;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Repositories
{
    public class ProductSizeRepository : BaseRepository<ProductSizeEntity>, IProductSizeRepository
    {
        private readonly ManeroDbContext _context;
        public ProductSizeRepository(ManeroDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task AddRangedAsync(ICollection<ProductSizeEntity> entities)
        {
            await _context.ProductSizes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
