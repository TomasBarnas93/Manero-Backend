using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReviewResponse>> GetAllAsync()
        {
            return await _reviewService.GetAllAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _reviewService.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPost("{productId}")]
        public async Task<IActionResult> CreateAsync(Guid productId, [FromBody] ReviewRequest review)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var result = await _reviewService.CreateAsync(productId, review);
            
            if(result == null!)
                return NotFound();

            return Created("", result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ReviewRequest review)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var result = await _reviewService.UpdateAsync(id, review);
            
            if(result is null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _reviewService.RemoveAsync(id);
            
            if(!result)
                return NotFound();

            return Ok(result);
        }

    }
}
