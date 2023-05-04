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
    
    public ReviewService(ManeroDbContext dbContext, IReviewRepository baseRepository, IProductRepository productRepository ) : base(dbContext, baseRepository)
    {
        _baseRepository = baseRepository;
        _productRepository = productRepository;
    }

    public async Task<ReviewResponse> CreateAsync(Guid productId, ReviewRequest review)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
            return null!;
        
        ValidateModel(ref review);
        
        ReviewEntity reviewEntity = review;

        reviewEntity.Product = product;
        
        var response =  await _baseRepository.CreateAsync(reviewEntity);
        
        await AdjustStarRating(productId);
        
        return response;
    }
    
    public override async Task<ReviewResponse?> UpdateAsync(Guid id, ReviewRequest review)
    {
        ValidateModel(ref review);
        
        var result = await base.UpdateAsync(id, review);
        
        if(result is null)
            return null;

        var reviewEntity = await _baseRepository.SearchSingleAsync(x => x.Id == id);
        
        await AdjustStarRating(reviewEntity!.ProductId);
        
        return result;
    }

    public override async Task<bool> RemoveAsync(Guid id)
    {
        await AdjustStarRating((await _baseRepository.SearchSingleAsync(x => x.Id == id))!.ProductId);
        
        return await base.RemoveAsync(id);
    }

    private void ValidateModel(ref ReviewRequest review)
    {
        if (review.StarRating > 5)
            review.StarRating = 5;
        else if(review.StarRating < 0)
            review.StarRating = 0;
    }

    private async Task AdjustStarRating(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        
        if (product is null)
            return;
        
        var reviews = await _baseRepository.SearchAsync(x => x.ProductId == id);
        
        product.StarRating = (int)Math.Round(product.Reviews.Average(r => r.StarRating), 0);
        
        await _productRepository.UpdateAsync(product);
    }

}