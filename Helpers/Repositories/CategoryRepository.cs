using Manero_Backend.Contexts;
using Manero_Backend.Models.Entities;
using Manero_Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity>
    {
        private readonly ManeroDbContext _context;
        public CategoryRepository(ManeroDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<CategoryEntity> GetCategoryByNameAsync(string name)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
        {
            return await _context.Category.ToListAsync();
        }
    }
}
