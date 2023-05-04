using Manero_Backend.Contexts;
using Manero_Backend.Helpers.Factory;
using Manero_Backend.Models.Dtos.Category;
using Manero_Backend.Models.Dtos.Product;
using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Dtos.Tag;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Mapster;

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

		product = await AdjustModel(product, entity);

		return await _productRepository.CreateAsync(product);
	}

	public override async Task<ProductResponse?> UpdateAsync(Guid id, ProductRequest entity)
	{
		var tempEntity = await _productRepository.GetByIdAsync(id);

		if (tempEntity is null)
			return null;

		tempEntity = await AdjustModel(tempEntity, entity);

		return await _productRepository.UpdateAsync(tempEntity);

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

	private async Task<ProductEntity> AdjustModel(ProductEntity entity, ProductRequest request)
	{
		if(request.Name != "")
			entity.Name = request.Name;
		
		if(request.Description != "")
			entity.Description = request.Description;
		
		if(request.Color != "")
			entity.Color = request.Color;
		
		if(request.Size != "")
			request.Size = entity.Size;
		
		if(request.Price != 0)
			entity.Price = request.Price;
		
		if(request.ImageUrl != "")
			entity.ImageUrl = request.ImageUrl;
		
		if(request.Category != "")
			entity.Category = await _categoryService.GetOrCreateAsync(CategoryFactory.CreateRequest(request.Category));
		
		if(request.Tag != "")
			entity.Tag = await _tagService.GetOrCreateAsync(TagFactory.CreateRequest(request.Tag));
		
		return entity;
	}
}
