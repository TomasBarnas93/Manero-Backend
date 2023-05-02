using Manero_Backend.Contexts;
using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Services;

public class ReviewService : BaseService<ReviewRequest, ReviewResponse, ReviewEntity>, IReviewService
{
    public ReviewService(ManeroDbContext dbContext, IReviewRepository baseRepository) : base(dbContext, baseRepository)
    {
    }
}