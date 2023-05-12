using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services
{
    public interface IProductSizeService
    {
        public Task AddRangedAsync(ICollection<ProductSizeEntity> entities);
    }
}
