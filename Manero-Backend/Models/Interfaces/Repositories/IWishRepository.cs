using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Repositories
{
    public interface IWishRepository : IBaseRepository<WishEntity>
    {
        public Task<bool> ExistsAsync(Guid productId, string userId);
    }
}
