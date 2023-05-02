using System.Linq.Expressions;
using Manero_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Models.Interfaces.Services;

public interface IBaseService<TRequest, TResponse, TEntity> 
    where TResponse : class 
    where TRequest : class
    where TEntity : class
{
    Task<IEnumerable<TResponse>> GetAllAsync();
    Task<TResponse> CreateAsync(TRequest entity);
    Task<TResponse?> UpdateAsync(Guid id,TRequest entity);
    Task<bool> RemoveAsync(Guid id);
    Task<TResponse?> GetByIdAsync(Guid id);
    Task<TResponse?> SearchSingleAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TResponse?>> SearchAsync(Expression<Func<TEntity, bool>> predicate);

}