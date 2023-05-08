using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services;

public interface IReviewService : IBaseService<ReviewRequest, ReviewResponse, ReviewEntity>
{
    public Task<ReviewResponse> CreateAsync(Guid productId, ReviewRequest review, string email);
    public Task<ReviewResponse?> UpdateAsync(Guid id, ReviewRequest review, string email);
}