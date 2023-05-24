using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Schemas.Order;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Models.Interfaces.Services
{
    public interface IOrderService : IBaseService<OrderEntity>
    {
        public Task<IActionResult> CreateAsync(OrderSchema schema, string userId);
        public Task<IActionResult> GetHistoryAsync(string userId);
        public Task<IActionResult> GetStatusAsync(string userId, Guid orderId);
        public Task<IActionResult> CancelAsync(OrderCancelSchema schema);
    }
}
