using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Entities;

namespace Manero_Backend.Helpers.Factory;

public class ReviewFactory
{
    public static ReviewEntity CreateEntity(ReviewRequest review)
    {
        return new ReviewEntity()
        {
            UserName = review.UserName,
            StarRating = review.StarRating,
            Description = review.Description,
        };
    }

    public static ReviewResponse CreateResponse(ReviewEntity response)
    {
        return new ReviewResponse
        {
            Id = response.Id,
            UserName = response.UserName,
            StarRating = response.StarRating,
            Description = response.Description,
        };
    }
}