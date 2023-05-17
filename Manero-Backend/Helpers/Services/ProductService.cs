using Manero_Backend.Contexts;
using Manero_Backend.Helpers.Factory;
using Manero_Backend.Helpers.JWT;
using Manero_Backend.Models.Dtos.Category;
using Manero_Backend.Models.Dtos.Product;
using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Dtos.Tag;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Product;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Formats.Asn1;

namespace Manero_Backend.Helpers.Services;

public class ProductService : BaseService<ProductRequest, ProductResponse, ProductEntity>, IProductService

{
    private readonly IProductRepository _productRepository;
    private readonly ITagService _tagService;
    private readonly ICategoryService _categoryService;
    private readonly IReviewService _reviewService;

	private readonly ITagProductService _tagProductService;
	private readonly IProductColorService _productColorService;
	private readonly IProductSizeService _productSizeService;
	private readonly IWishService _wishService;
    public ProductService(ManeroDbContext dbContext, IProductRepository baseRepository, ITagService tagService, ICategoryService categoryService, IReviewService reviewService, ITagProductService tagProductService, IProductColorService productColorService, IProductSizeService productSizeService, IWishService wishService) : base(dbContext, baseRepository)

    {
        _productRepository = baseRepository;
        _tagService = tagService;
        _categoryService = categoryService;
        _reviewService = reviewService;
        _tagProductService = tagProductService;
        _productColorService = productColorService;
        _productSizeService = productSizeService;
		_wishService = wishService;
    }


	public async Task<IActionResult> GetByGuid(Guid guid, string userId)
	{
		ProductEntity entity = await _productRepository.GetByGuid(guid);
		if (entity == null)
			return HttpResultFactory.NotFound("");

		var productDto = (ProductDto)entity;
		productDto.Liked = await _wishService.ExistsAsync(guid, userId);

        return  HttpResultFactory.Ok(productDto);
	}

    public async Task<IActionResult> GetByOptions(IEnumerable<ProductOptionSchema> schema, string userId)
    {
		List<object> result = new List<object>();

        foreach (var productOption in schema)
		{
			if (productOption.TagId == null && productOption.CategoryId == null) //1.
			{
				result.Add(new { Option = productOption, Result = string.Empty, ErrorMessage = "Provide at least a TagId or a CategoryId." });

				continue;
			}

            if (productOption.TagId != null && productOption.CategoryId != null)
            {
                if ((await _categoryService.ExistsAsync((Guid)productOption.CategoryId) == false) || (await _tagService.ExistsAsync((Guid)productOption.TagId) == false))
                {
                    result.Add(new { Option = productOption, Result = string.Empty, ErrorMessage = "Either TagId or CategoryId or both do not exist." });
                    continue;
                }
            }

            result.Add(new { Option = productOption, Result = (await _productRepository.GetByOption(productOption)).Select(x => 
			{
				var productMinDto = (ProductMinDto)x;
				productMinDto.Liked = x.WishList.Where(y => y.AppUserId == userId && y.ProductId == x.Id).FirstOrDefault() != null;
                return productMinDto;
            }), ErrorMessage = string.Empty });
        }

		return HttpResultFactory.Ok(result);
    }

	


	public async Task<ProductEntity> CreateAsync(ProductSchema schema)
	{
		
        ProductEntity entity = await _productRepository.CreateAsync(schema);

		await _tagProductService.AddRangedAsync(schema.TagIds.Select(x => new TagProductEntity() { TagId = x, ProductId = entity.Id }).ToList());
		await _productColorService.AddRangedAsync(schema.ColorIds.Select(x => new ProductColorEntity() { ColorId = x, ProductId = entity.Id }).ToList());
		await _productSizeService.AddRangedAsync(schema.SizeIds.Select(x => new ProductSizeEntity() { SizeId = x, ProductId = entity.Id}).ToList());


		return null;
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
		IEnumerable<ProductResponse> list = null;//await _productRepository.SearchAsync(x=>x.Tag.Name.ToLower() == tag.Name.ToLower());
		
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
		
		if (!list.Any() || list == null!)
			return null!;
		
		return list.Adapt<IEnumerable<ReviewResponse>>();
	}

	private async Task<ProductEntity> AdjustModel(ProductEntity entity, ProductRequest request)
	{
		return null;
		if(request.Name != "")
			entity.Name = request.Name;
		
		if(request.Description != "")
			entity.Description = request.Description;
		
		if(request.Color != "")
			//entity.Color = request.Color;
		
		if(request.Size != "")
			//request.Size = entity.Size;
		
		if(request.Price != 0)
			entity.Price = request.Price;
		
		if(request.ImageUrl != "")
			entity.ImageUrl = request.ImageUrl;
		
		if(request.Category != "")
			entity.Category = await _categoryService.GetOrCreateAsync(CategoryFactory.CreateRequest(request.Category));
		
		if(request.Tag != "")
			//entity.Tag = await _tagService.GetOrCreateAsync(TagFactory.CreateRequest(request.Tag));
		
		return null;
	}





    public async Task FillDataAsync()
	{
		await _productRepository.FillDataAsync();
	}
}
