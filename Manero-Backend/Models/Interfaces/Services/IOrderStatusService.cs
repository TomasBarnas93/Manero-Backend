using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services
{
    public interface IOrderStatusService : IBaseService<OrderStatusEntity>
    {
        public Task<List<OrderStatusEntity>> CreateStatic(Guid orderId);
        public Task AddCanceledAsync(Guid orderId);
    }
}
