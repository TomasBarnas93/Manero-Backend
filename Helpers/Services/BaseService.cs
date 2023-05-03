using System.Linq.Expressions;
using Manero_Backend.Contexts;
using Manero_Backend.Models.Interfaces;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Manero_Backend.Helpers.Services;

public class BaseService<TRequest, TResponse, TEntity> : IBaseService<TRequest, TResponse, TEntity>
    where TResponse : class
    where TRequest : class
    where  TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly IBaseRepository<TEntity> _baseRepository;

    public BaseService(DbContext dbContext, IBaseRepository<TEntity> baseRepository)
    {
        _baseRepository = baseRepository;
        _dbSet = dbContext.Set<TEntity>();
    }


    public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
    {
        var listEntity = await _baseRepository.GetAllAsync();
        
        var response = listEntity.Adapt<IEnumerable<TResponse>>();
        
        return response;
    }

    public virtual async Task<TResponse> CreateAsync(TRequest entity)
    {
        var entityToCreate = entity as TEntity;
        
        var entityCreated = await _baseRepository.CreateAsync(entityToCreate!);
        
        var response = entityCreated as TResponse;
        
        return response!;
    }

    public virtual async Task<TResponse?> UpdateAsync(Guid id,TRequest entity)
    {
        var tempEntity = await _baseRepository.GetByIdAsync(id);

        if (tempEntity is null)
            return null;
        
        tempEntity = entity as TEntity;
        
        return await _baseRepository.UpdateAsync(tempEntity!) as TResponse;
    }

    public virtual async Task<bool> RemoveAsync(Guid id)
    {
        var entityToRemove = await _baseRepository.GetByIdAsync(id);

        if (entityToRemove is null)
            return false;
        
        return await _baseRepository.RemoveAsync(entityToRemove!);
    }

    public virtual async Task<TResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _baseRepository.GetByIdAsync(id);

        var response = entity.Adapt<TResponse>();

        return response;
    }


    public virtual async Task<TResponse?> SearchSingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await _baseRepository.SearchSingleAsync(predicate);
        
        return entity as TResponse;
    }

    public virtual async Task<IEnumerable<TResponse?>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var list = await _baseRepository.SearchAsync(predicate);
        
        var response = list.Adapt<IEnumerable<TResponse>>();
        
        return response;
    }
}