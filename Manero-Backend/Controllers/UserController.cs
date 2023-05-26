using Manero_Backend.Helpers.Factory;
using Manero_Backend.Helpers.JWT;
using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);
                if (userId == null)
                    return NotFound();

                var user = _userManager.FindByIdAsync(userId);
                UserProfileDto profile = user.Result;

                return HttpResultFactory.Ok(profile);
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }
        }
    }
}
