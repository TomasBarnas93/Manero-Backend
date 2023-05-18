using System.Net;
using Manero_Backend.Helpers.JWT;
using Manero_Backend.Models.Dtos.Authentication;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterSchema schema)
        {
            if(!ModelState.IsValid)
                return BadRequest("");

            try
            {
                return await _authService.RegisterAsync(schema);
            }
            catch(Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                return await _authService.LoginAsync(schema);
            }
            catch(Exception e) //Ilogger
            {
                return StatusCode(500,"");
            }
        }


        [HttpPost("getcode")]
        [Authorize]
        public async Task<IActionResult> GetCode(PhoneNumberSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                //Send sms with code add later.
                

                //Set phone number.
                var userId = JwtToken.GetIdFromClaim(HttpContext);

                return await _authService.SetPhoneNumberAsync(userId, schema);
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }
        }

        [HttpPost("validatecode")]
        [Authorize]
        public async Task<IActionResult> ValidateOtp(CodeSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);

                return await _authService.ValidatePhoneNumber(userId, schema); 
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }

        }




    }
}
