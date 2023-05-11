using Manero_Backend.Contexts;
using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Services;

public class ReviewService : BaseService<ReviewRequest, ReviewResponse, ReviewEntity>, IReviewService
{
    private readonly IProductRepository _productRepository;
    private readonly IReviewRepository _baseRepository;
    private readonly UserManager<AppUser> _userManager;

    public ReviewService(ManeroDbContext dbContext, IReviewRepository baseRepository, IProductRepository productRepository, UserManager<AppUser> userManager) : base(dbContext, baseRepository)
    {
        _baseRepository = baseRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }

    public async Task<ReviewResponse> CreateAsync(Guid productId, ReviewRequest review, string email)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
            return null!;
        
        ValidateModel(ref review);
        
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return null!;
        
        
        ReviewEntity reviewEntity = review;

        reviewEntity.Product = product;
        reviewEntity.AppUser = user;
        
        var response =  await _baseRepository.CreateAsync(reviewEntity);
        
        await AdjustStarRating(productId);
        
        return response;
    }
    
    public async Task<ReviewResponse?> UpdateAsync(Guid id, ReviewRequest review, string email)
    {
        ValidateModel(ref review);

        
        var user = await _userManager.FindByEmailAsync(email);
        //Check if user has this review
        var reviewEntity = await _baseRepository.SearchSingleAsync(x => x.Id == id && x.AppUserId == user!.Id);
        if (reviewEntity is null) return null;

        var result = await base.UpdateAsync(id, review);
        
        if(result is null)
            return null;

        
        await AdjustStarRating(reviewEntity!.ProductId);
        
        return result;
    }

    public override async Task<bool> RemoveAsync(Guid id)
    {
        
        var productId = (await _baseRepository.SearchSingleAsync(x => x.Id == id))!.ProductId;
        await AdjustStarRating(productId);
        
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
        
        //product.StarRating = (int)Math.Round(product.Reviews.Average(r => r.StarRating), 0);
        
        await _productRepository.UpdateAsync(product);
    }

}