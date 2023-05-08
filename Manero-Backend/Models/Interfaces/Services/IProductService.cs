using Manero_Backend.Models.Dtos.Category;
using Manero_Backend.Models.Dtos.Product;
using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Dtos.Tag;
using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services;

public interface IProductService: IBaseService<ProductRequest, ProductResponse, ProductEntity> 
{
    Task<IEnumerable<ProductResponse?>> GetByTagAsync(TagRequest tag);
    Task<IEnumerable<ProductResponse?>> GetByCategoryAsync(CategoryRequest createRequest);
    Task<IEnumerable<ReviewResponse>> GetReviewsAsync(Guid id);
}