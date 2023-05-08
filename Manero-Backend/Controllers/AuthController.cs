using System.Net;
using Manero_Backend.Models.Dtos.Authentication;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Constructor
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        #endregion

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _authService.GetAllAsync());
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterForm form)
        {
            var result = await _authService.RegisterAsync(form);

            return result switch
            {
                HttpStatusCode.Created => Created("", null),
                HttpStatusCode.Conflict => Conflict(),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => BadRequest()
            };
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginForm form)
        {
            var result = await _authService.LoginAsync(form);
            
            return result switch
            {
                null => BadRequest(),
                _ => Ok(result)
            };
        }
        
        //You dont need to use this to logout. You can just delete the token from the client side.
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            
            return result ? Ok() : BadRequest();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _authService.DeleteAsync(id);
            return Ok();
        }

    }
}
