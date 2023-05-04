using Manero_Backend.Contexts;
using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Services;

public class ReviewService : BaseService<ReviewRequest, ReviewResponse, ReviewEntity>, IReviewService
{
    private readonly IProductRepository _productRepository;
    private readonly IReviewRepository _baseRepository;
    public ReviewService(ManeroDbContext dbContext, IReviewRepository baseRepository, IProductRepository productRepository) : base(dbContext, baseRepository)
    {
        _baseRepository = baseRepository;
        _productRepository = productRepository;
    }

    public async Task<ReviewResponse> CreateAsync(Guid productId, ReviewRequest review)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
            return null!;
        
        ReviewEntity reviewEntity = review;

        reviewEntity.Product = product;
        
        var response =  await _baseRepository.CreateAsync(reviewEntity);
        
        return response;

    }
}