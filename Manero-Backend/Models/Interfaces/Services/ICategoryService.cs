using Manero_Backend.Models.Dtos.Category;
using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services;

public interface ICategoryService : IBaseService<CategoryRequest, CategoryResponse, CategoryEntity> 
{
    public Task<CategoryEntity> GetOrCreateAsync(CategoryRequest entityCategory);
}