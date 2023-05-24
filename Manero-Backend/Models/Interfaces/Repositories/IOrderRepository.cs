using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<OrderEntity>
    {
        public Task<ICollection<OrderEntity>> GetAllIncludeAsync(string userId);
        public Task<OrderEntity> GetIncludeAsync(string userId, Guid orderId);
        public Task<OrderEntity> GetIncludeAsync(Guid orderId);
    }
}
