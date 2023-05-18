using System.Security.Claims;
using Manero_Backend.Helpers.JWT;
using Manero_Backend.Models.Dtos.Review;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(ReviewSchema schema)
        {
            if(!ModelState.IsValid)
                return BadRequest("");

            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);

                return await _reviewService.CreateAsync(schema, userId);
            }
            catch(Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }
        }

        
    }
}
