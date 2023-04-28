using Manero_Backend.Contexts;
using Manero_Backend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();

        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var result = await _context.FindAsync<TEntity>(id);

            return result!;

        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _context?.Set<TEntity>().Remove(entity);
            await _context!.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context!.SaveChangesAsync();
            return entity;
        }
        #endregion

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

    }
}