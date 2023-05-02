using System.Linq.Expressions;
using Manero_Backend.Models.Interfaces;
using Manero_Backend.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
	    where TEntity : class
    {
        #region Constructor
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
	        _dbContext = dbContext;
	        _dbSet = _dbContext.Set<TEntity>();
        }
        #endregion

        #region CRUD

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
			var result = await _dbSet.ToListAsync();
			return result;
		}
        
        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var result = await _dbSet.FindAsync(id);

            return result!;
        }

        public async Task<TEntity?> SearchSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
	       return await _dbSet.SingleOrDefaultAsync(predicate);
        }
		
        public async Task<IEnumerable<TEntity?>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
	        return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
			await _dbSet.AddAsync(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

        public virtual async Task<bool> RemoveAsync(TEntity entity)
        {
	        _dbSet.Remove(entity);
			var result = await _dbContext.SaveChangesAsync();
			
			return result > 0;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
			_dbSet.Update(entity);	
			await _dbContext.SaveChangesAsync(); 

			return entity;
		}
        #endregion
	}
}