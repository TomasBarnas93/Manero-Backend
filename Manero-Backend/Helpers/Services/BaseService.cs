using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Manero_Backend.Helpers.Services;

public class BaseService<TEntity> : IBaseService<TEntity> where  TEntity : class
{

    private readonly IBaseRepository<TEntity> _baseRepository;

    public BaseService(IBaseRepository<TEntity> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _baseRepository.ExistsAsync(id);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _baseRepository.CountAsync(predicate);
    }

    public async Task AddRangedAsync(ICollection<TEntity> entities)
    {
        await _baseRepository.AddRangedAsync(entities);
    }
}