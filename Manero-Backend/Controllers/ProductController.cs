using Manero_Backend.Helpers.Factory;
using Manero_Backend.Models.Dtos.Product;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Product;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("v1/api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpPost("products")]
		public async Task<IActionResult> GetByOptions(IEnumerable<ProductOptionSchema> schema)
		{
			if (!ModelState.IsValid)
				return BadRequest("");

			try
			{


				return Ok(await _productService.GetByOptions(schema));
			}
			catch(Exception e) //Log
			{
				return StatusCode(500, "");
			}
		}

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");


            try
            {
				await _productService.CreateAsync(schema);


                return Created("","");
            }
            catch (Exception e) //Log
            {
                return StatusCode(500, "");
            }
        }



        [HttpGet]
		public async Task<IEnumerable<ProductResponse>> GetAllAsync()
		{
			return await _productService.GetAllAsync();
		}
		
		
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var result = await _productService.GetByIdAsync(id);

			if (result is null)
				return NotFound();

			return Ok(result);
		}
		
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(Guid id, ProductRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var result = await _productService.UpdateAsync(id, request);

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

		[HttpGet("test")]
		public async Task<IActionResult> TestAsync()
		{
			await _productService.FillDataAsync();

			return Ok("");
		}
	}
}
