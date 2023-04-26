using Manero_Backend.Contexts;
using Manero_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Repositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		#region Constructor
		private readonly ManeroDbContext _context;

		public BaseRepository(ManeroDbContext context)
		{
			_context = context;
		}
		#endregion

		#region Get
		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>().ToListAsync();

		}
		public async Task<TEntity> GetByIdAsync(int id)
		{
			return await _context.FindAsync<TEntity>(id);

		}
		#endregion

		#region CRUD

		public async Task<TEntity> CreateAsync(TEntity entity)
		{
			_context.Add(entity);
			await _context.SaveChangesAsync();

			return entity;
		}

		public async Task DeleteAsync(TEntity entity)
		{
			_context?.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
		#endregion

	}
}
