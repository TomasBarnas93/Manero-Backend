using Manero_Backend.Contexts;
using Manero_Backend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

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

        #region CRUD

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
			try
			{
				var result = await _context.Set<TEntity>().ToListAsync();
				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return null!;

		}
        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var result = await _context.FindAsync<TEntity>(id);

            return result!;

        }
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
			try
			{
				_context.Set<TEntity>().Add(entity);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return entity;
		}

        public virtual async Task RemoveAsync(TEntity entity)
        {
            try
            {
				_context?.Set<TEntity>().Remove(entity);
				await _context!.SaveChangesAsync();
			}
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }  
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
			try
			{
				_context.Set<TEntity>().Update(entity);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return entity;
		}
        #endregion

        public virtual async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
				return await _context.Set<TEntity>().Where(predicate).ToListAsync();
			}
			catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
            return null!;
        }

		public async Task<IEnumerable<TEntity>> GetAllByTag(string tag)
		{
			var result = await _context.FindAsync<IEnumerable<TEntity>>(tag);

			return result!;

		}
		public async Task<IEnumerable<TEntity>> GetAllByGenre(string genre)
		{
			var result = await _context.FindAsync<IEnumerable<TEntity>>(genre);

			return result!;

		}
	}
}