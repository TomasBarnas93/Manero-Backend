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
        public async Task<IEnumerable<ReviewResponse>> GetAll()
        {
            return await _reviewService.GetAllAsync();
        }
        
        [HttpPost("{productId}")]
        public async Task<IActionResult> CreateReview(Guid productId, [FromBody] ReviewRequest review)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var result = await _reviewService.CreateAsync(productId, review);
            
            if(result == null!)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] ReviewRequest review)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var result = await _reviewService.UpdateAsync(id, review);
            
            if(result is null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var result = await _reviewService.RemoveAsync(id);
            
            if(!result)
                return NotFound();

            return Ok(result);
        }

    }
}
