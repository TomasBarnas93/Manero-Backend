using System.Net;
using Manero_Backend.Helpers.JWT;
using Manero_Backend.Models.Dtos.Authentication;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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

        [HttpGet("validatetoken")]
        [Authorize]
        public async Task<IActionResult> ValidateToken()
        {
            return NoContent();
        }




        [Obsolete("Not ready yet")]
        [HttpGet("getpasswordcode")]
        public async Task<IActionResult> GetPasswordCodeAsync(ForgotPasswordSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                return await _authService.GetPasswordCodeAsync(schema);
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, e);
            }

        }
        [Obsolete("Not ready yet")]
        [HttpGet("resetcode/{code}")]
        public async Task<IActionResult> ValidatePasswordCodeAsync(string code)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {

                return await _authService.ValidatePasswordCode(code);
                //return Redirect("https://localhost:7164/passwordreset/" + code); //frontend to reset password or to change password
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, e);
            }
        }

        [Obsolete("Not ready yet")]
        [HttpGet("changepassword")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");
            try
            {

                return await _authService.ChangePasswordAsync(schema);
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, e);
            }
        }
    }
}
