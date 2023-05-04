using Manero_Backend.Helpers.Factory;
using Manero_Backend.Helpers.Services;
using Manero_Backend.Models.Dtos;
using Manero_Backend.Models.Dtos.Product;
using Manero_Backend.Models.Dtos.Tag;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IEnumerable<ProductResponse>> GetAllAsync()
		{
			return await _productService.GetAllAsync();
		}
		
		[HttpPost]
		public async Task<IActionResult> CreateAsync(ProductRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			
			var result = await _productService.CreateAsync(request);

			return Created("", result);
		}
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var result = await _productService.GetByIdAsync(id);

			if (result is null)
				return NotFound();

			return Ok(result);
		}
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveAsync(Guid id)
		{
			var result = await _productService.RemoveAsync(id);

			if (!result)
				return NotFound();

			return Ok();
		}
		
		[HttpGet("tag/{tag}")]
		public async Task<IActionResult> GetByTagAsync(string tag)
		{
			var result = await _productService.GetByTagAsync(TagFactory.CreateRequest(tag));

			if (result is null)
				return NoContent();


			return Ok(result);
		}
		
		[HttpGet("category/{category}")]
		public async Task<IActionResult> GetByCategoryAsync(string category)
		{
			var result = await _productService.GetByCategoryAsync(CategoryFactory.CreateRequest(category));

			if (result == null!)
				return NoContent();


			return Ok(result);
		}

		[HttpGet("reviews/{id}")]
		public async Task<IActionResult> GetReviewsAsync(Guid id)
		{
			var result = await _productService.GetReviewsAsync(id);

			if (result == null!)
				return NoContent();
			
			return Ok(result);
		}
	}
}
