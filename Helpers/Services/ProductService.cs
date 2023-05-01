using Manero_Backend.Helpers.Repositories;
using Manero_Backend.Models.Dtos;
using Manero_Backend.Models.Entities;
using Manero_Backend.Repositories;

namespace Manero_Backend.Helpers.Services;

public class ProductService
{
    private readonly ProductRepository _repository;
    private readonly TagRepository _tagRepository;
    private readonly CategoryRepository _categoryRepository;

	public ProductService(ProductRepository repository, TagRepository tagRepository, CategoryRepository categoryRepository)
	{
		_repository = repository;
		_tagRepository = tagRepository;
		_categoryRepository = categoryRepository;
	}

	public async Task<ProductResponse> GetProductAsync(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);
        return result;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(x => (ProductResponse)x).ToList();
    }


    public async Task<ProductResponse> CreateAsync(ProductRequest productRequest)
    {
		var existingTag = await _tagRepository.GetTagByNameAsync(productRequest.Tag.Name.ToLower());
		var existingCategory = await _categoryRepository.GetCategoryByNameAsync(productRequest.Category.Name.ToLower());

		if (existingTag == null && existingCategory == null)
		{
			var newTag = new TagEntity { Name = productRequest.Tag.Name.ToLower() };
			var newCategory = new CategoryEntity { Name = productRequest.Category.Name.ToLower() };
			productRequest.Tag = newTag;
            productRequest.Category = newCategory;
		}
		else
		{
			productRequest.Tag = existingTag;
            productRequest.Category = existingCategory;
		}

		var result = await _repository.CreateAsync(productRequest);
		return result;
	}


    public async Task<ProductResponse> UpdateAsync(Guid productId, ProductRequest productRequest)
    {
        var existingProduct = await _repository.GetByIdAsync(productId);

        if (existingProduct == null)
        {
            return null;
        }

        existingProduct.Name = productRequest.Name;
        existingProduct.Description = productRequest.Description;
        existingProduct.Color = productRequest.Color;
        existingProduct.Size = productRequest.Size;
        existingProduct.Price = productRequest.Price;
        existingProduct.StarRating = productRequest.StarRating;
        existingProduct.ImageUrl = productRequest.ImageUrl;
        existingProduct.Tag = productRequest.Tag;
        existingProduct.Category = productRequest.Category;

        var updatedProduct =  await _repository.UpdateAsync(existingProduct);

        return updatedProduct;
    }


    public async Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        await _repository.RemoveAsync(product);
    }

    public async Task<IEnumerable<ProductResponse>> SearchAsync(string searchTerm)
    {
        var products = await _repository.GetAllAsync();

        var list = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())||
        x.Description.ToLower().Contains(searchTerm.ToLower()));

        var result = new List<ProductResponse>();
        foreach (var product in products)
        {
            result.Add(product);
        }
        return result;
    }

	public async Task<IEnumerable<ProductResponse>> GetAllByTagAsync(string tag)
	{
		var resultlist = await _repository.GetAllByTag(tag);


		var result = new List<ProductResponse>();
		foreach (var product in resultlist)
		{
			result.Add(product);
		}
		return result;

	}

	public async Task<IEnumerable<ProductResponse>> GetAllByGenreAsync(string tag)
	{
		var resultlist = await _repository.GetAllByGenre(tag);


		var result = new List<ProductResponse>();
		foreach (var product in resultlist)
		{
			result.Add(product);
		}
		return result;

	}
}
