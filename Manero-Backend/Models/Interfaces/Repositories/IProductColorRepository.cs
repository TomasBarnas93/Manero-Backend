using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Repositories
{
    public interface IProductColorRepository : IBaseRepository<ProductColorEntity>
    {
        public Task AddRangedAsync(ICollection<ProductColorEntity> entities);
    }
}
