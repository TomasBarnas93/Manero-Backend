using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Repositories
{
    public interface IProductSizeRepository : IBaseRepository<ProductSizeEntity>
    {
        public Task AddRangedAsync(ICollection<ProductSizeEntity> entities);
    }
}
