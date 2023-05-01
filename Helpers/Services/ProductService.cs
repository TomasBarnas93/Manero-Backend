using Azure;
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
        else if (existingCategory == null )
        {
            var newCategory = new CategoryEntity { Name = productRequest.Category.Name.ToLower() };
            productRequest.Category = newCategory;
            productRequest.Tag = existingTag;
        }
        else if(existingTag == null)
        {
            var newTag = new TagEntity { Name = productRequest.Tag.Name.ToLower() };
            productRequest.Tag = newTag;
            productRequest.Category = existingCategory;
        }
        else
		{
			productRequest.Tag = existingTag;
            productRequest.Category = existingCategory;
		}

		var result = await _repository.CreateAsync(productRequest);
		return result;
	}


    public async Task<ProductResponse> UpdateAsync(Guid productId, ProductRequest newProductRequest)
    {

        var tagCheck = await _tagRepository.GetTagByNameAsync(newProductRequest.Tag.Name.ToLower());
        var categoryCheck = await _categoryRepository.GetCategoryByNameAsync(newProductRequest.Category.Name.ToLower());

        var existingProduct = await _repository.GetByIdAsync(productId);

        //Om den uppdaterade Produkten saknar värden för tag & category så skapas det nya. 
        if (categoryCheck == null && tagCheck == null)
        {
            var newTag = new TagEntity { Name = newProductRequest.Tag.Name.ToLower() };
            var newCategory = new CategoryEntity { Name = newProductRequest.Category.Name.ToLower() };
            existingProduct.Tag = newTag;
            existingProduct.Category = newCategory;
        }

        else if (tagCheck == null)
        {
            var newTag = new TagEntity { Name = newProductRequest.Tag.Name.ToLower() };
            existingProduct.Tag = newTag;

            existingProduct.Category = newProductRequest.Category;


        }
        else if(categoryCheck == null)
        {
            var newCategory = new CategoryEntity { Name = newProductRequest.Category.Name.ToLower() };
            existingProduct.Category = newCategory;

            existingProduct.Tag = newProductRequest.Tag;
        }
        else
        {
            existingProduct.Tag = newProductRequest.Tag;
            existingProduct.Category = newProductRequest.Category;
        }


        existingProduct.Name = newProductRequest.Name;
        existingProduct.Description = newProductRequest.Description;
        existingProduct.Color = newProductRequest.Color;
        existingProduct.Size = newProductRequest.Size;
        existingProduct.Price = newProductRequest.Price;
        existingProduct.StarRating = newProductRequest.StarRating;
        existingProduct.ImageUrl = newProductRequest.ImageUrl;


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
