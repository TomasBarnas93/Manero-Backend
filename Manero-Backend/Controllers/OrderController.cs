using Manero_Backend.Helpers.JWT;
using Manero_Backend.Helpers.Services;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrderSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);

                return await _orderService.CreateAsync(schema, userId);
            }
            catch (Exception e) //Ilogger
            {
                Console.WriteLine(e);
                return StatusCode(500, "");
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistoryAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);

                return await _orderService.GetHistoryAsync(userId);
            }
            catch (Exception e) //Ilogger
            {
                Console.WriteLine(e);
                return StatusCode(500, "");
            }
        }

        [HttpGet("status/{orderid}")]
        public async Task<IActionResult> GetStatusAsync(Guid orderId)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                var userId = JwtToken.GetIdFromClaim(HttpContext);

                return await _orderService.GetStatusAsync(userId, orderId);
            }
            catch (Exception e) //Ilogger
            {
                Console.WriteLine(e);
                return StatusCode(500, "");
            }

        }

        [HttpPost("cancel")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelAsync(OrderCancelSchema schema)
        {
            if (!ModelState.IsValid)
                return BadRequest("");

            try
            {
                return await _orderService.CancelAsync(schema);
            }
            catch (Exception e) //Ilogger
            {
                Console.WriteLine(e);
                return StatusCode(500, "");
            }
        }
    }
}
