using Manero_Backend.Contexts;
using Manero_Backend.Models.Entities;
using Manero_Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Repositories
{
	public class TagRepository : BaseRepository<TagEntity>
	{
		private readonly ManeroDbContext _context;
		public TagRepository(ManeroDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<TagEntity> GetTagByNameAsync(string name)
		{
			return await _context.Tags.FirstOrDefaultAsync(x => x.Name == name);
		}

		public async Task<IEnumerable<TagEntity>> GetAllAsync()
		{
			return await _context.Tags.ToListAsync();
		}
	}
}
