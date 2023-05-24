using Manero_Backend.Helpers.Services;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusTypeController : ControllerBase
    {
        private readonly IOrderStatusTypeService _orderStatusTypeService;

        public OrderStatusTypeController(IOrderStatusTypeService orderStatusTypeService)
        {
            _orderStatusTypeService = orderStatusTypeService;
        }

        [HttpGet]
        [Route("~/v1/api/orderstatustypes")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return await _orderStatusTypeService.GetAllAsync();
            }
            catch (Exception e) //Ilogger
            {
                return StatusCode(500, "");
            }
        }
    }
}
