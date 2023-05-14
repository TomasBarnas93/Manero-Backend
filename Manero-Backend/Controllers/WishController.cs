using Manero_Backend.Helpers.JWT;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Wish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]

    public class WishController : ControllerBase
    {
        private readonly IWishService _wishService;

        public WishController(IWishService wishService)
        {
            _wishService = wishService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddAsync(WishSchema schema)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);

                return await _wishService.AddAsync(schema, userId);
            }
            catch(Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);

                return await _wishService.GetAllAsync(userId);
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }

        }

    }
}
