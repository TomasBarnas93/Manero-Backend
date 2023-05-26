using Manero_Backend.Helpers.Factory;
using Manero_Backend.Helpers.JWT;
using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Dtos.User;
using Manero_Backend.Models.Schemas.User;
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

                var user = await _userManager.FindByIdAsync(userId);
 
                return HttpResultFactory.Ok((UserProfileDto)user);
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(ProfilePutSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);
                if (userId == null)
                    return NotFound();

                var user = await _userManager.FindByIdAsync(userId);

                user.FirstName = schema.FirstName;
                user.LastName = schema.LastName;
                user.Email = schema.Email;
                user.PhoneNumber = schema.PhoneNumber;
                user.ImageUrl = schema.ImageUrl;
                user.Location = schema.Location;

                await _userManager.UpdateAsync(user);

                return HttpResultFactory.NoContent();
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }
        }

    }
}
