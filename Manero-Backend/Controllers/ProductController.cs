using Manero_Backend.Helpers.Factory;
using Manero_Backend.Helpers.JWT;
using Manero_Backend.Helpers.Services;
using Manero_Backend.Models.Dtos.Product;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

		[HttpGet("id/{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			if (!ModelState.IsValid)
				return BadRequest("");

			try
			{
				string userId = null;
				if (User.Identity.IsAuthenticated)
                    userId = JwtToken.GetIdFromClaim(HttpContext);
       

                return await _productService.GetByGuid(id, userId);
			}
			catch(Exception e)//ilogger
			{
				return StatusCode(500,"");
			}
		}


		[HttpGet("options")]
		public async Task<IActionResult> GetByOptions(IEnumerable<ProductOptionSchema> schema)
		{
			if (!ModelState.IsValid)
				return BadRequest("");

			try
			{
                string userId = null;
                if (User.Identity.IsAuthenticated)
                    userId = JwtToken.GetIdFromClaim(HttpContext);


                return await _productService.GetByOptions(schema, userId);
			}
			catch(Exception e) //Log
			{
                return StatusCode(500, "");
            }
		}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(ProductSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");


            try
            {

                return await _productService.CreateAsync(schema);
            }
            catch (Exception e) //Log
            {
                return StatusCode(500, "");
            }
        }



        [Obsolete("May not work as intended. DO NO USE !")]
        [HttpGet("test")]
		public async Task<IActionResult> TestAsync()
		{
			await _productService.FillDataAsync();

			return Ok("");
		}
	}
}
