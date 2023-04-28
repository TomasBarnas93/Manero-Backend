using Manero_Backend.Models.Dtos;
using Manero_Backend.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Services;

public class ProductService
{
    private readonly ProductRepository _repository;

    public ProductService(ProductRepository repository)
    {
        _repository=repository;
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

}
