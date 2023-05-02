using System.Linq.Expressions;
using Manero_Backend.Contexts;
using Manero_Backend.Helpers.Factory;
using Manero_Backend.Helpers.Repositories;
using Manero_Backend.Models.Dtos;
using Manero_Backend.Models.Dtos.Category;
using Manero_Backend.Models.Dtos.Product;
using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Dtos.Tag;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Services;

public class ProductService : BaseService<ProductRequest, ProductResponse, ProductEntity>, IProductService

{
    private readonly IProductRepository _productRepository;
    private readonly ITagService _tagService;
    private readonly ICategoryService _categoryService;
    private readonly IReviewService _reviewService;
    
    public ProductService(ManeroDbContext dbContext, IProductRepository baseRepository, ITagService tagService, ICategoryService categoryService, IReviewService reviewService) : base(dbContext, baseRepository)
    
    {
	    _productRepository = baseRepository;
	    _tagService = tagService;
	    _categoryService = categoryService;
	    _reviewService = reviewService;
    }
    
	public override async Task<ProductResponse> CreateAsync(ProductRequest entity)
	{
		ProductEntity product = entity;

		product.Category = await _categoryService.GetOrCreateAsync(CategoryFactory.CreateRequest(entity.Category));
		product.Tag = await _tagService.GetOrCreateAsync(TagFactory.CreateRequest(entity.Tag));

		return await _productRepository.CreateAsync(product);
	}

	public async Task<IEnumerable<ProductResponse?>> GetByTagAsync(TagRequest tag)
	{
		var list = await _productRepository.SearchAsync(x=>x.Tag.Name.ToLower() == tag.Name.ToLower());
		
		if (!list.Any())
			return null!;
		
		return list.Adapt<IEnumerable<ProductResponse>>();
	}

	public async Task<IEnumerable<ProductResponse?>> GetByCategoryAsync(CategoryRequest createRequest)
	{
		var list = await _productRepository.SearchAsync
			(x=>x.Category.Name.ToLower() == createRequest.Name.ToLower());

		if (!list.Any())
			return null!;
		
		
		return list.Adapt<IEnumerable<ProductResponse>>();
	}

	public async Task<IEnumerable<ReviewResponse>> GetReviewsAsync(Guid id)
	{
		var list = await _reviewService.SearchAsync(x=>x.ProductId == id);
		
		if (!list.Any())
			return null!;
		
		return list.Adapt<IEnumerable<ReviewResponse>>();
	}
}
