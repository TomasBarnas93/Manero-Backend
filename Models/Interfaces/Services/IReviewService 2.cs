using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services;

public interface IReviewService : IBaseService<ReviewRequest, ReviewResponse, ReviewEntity>
{
}