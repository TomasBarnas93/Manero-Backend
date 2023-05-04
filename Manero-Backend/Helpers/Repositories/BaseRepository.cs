using Manero_Backend.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

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
            try
            {
                var result = await _dbSet.ToListAsync();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _dbSet.FindAsync(id);

                return result!;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }

        public async Task<TEntity?> SearchSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _dbSet.SingleOrDefaultAsync(predicate);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }

        public async Task<IEnumerable<TEntity?>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return entity;
        }

        public virtual async Task<bool> RemoveAsync(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return entity;
        }
        #endregion
    }
}