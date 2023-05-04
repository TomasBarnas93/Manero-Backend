using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manero_Backend.Models.Dtos.Authentication;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("api/[controller]")]
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

        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterForm form)
        {
            var result = await _authService.RegisterAsync(form);
            
            if(!result)
                return BadRequest();
            
            return Ok();
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginForm form)
        {
            var result = await _authService.LoginAsync(form);
            
            if(result == null!)
                return BadRequest();
            
            return Ok(result);
        }
    }
}
