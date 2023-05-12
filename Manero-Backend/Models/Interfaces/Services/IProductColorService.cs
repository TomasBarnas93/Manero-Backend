using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services
{
    public interface IProductColorService
    {
        public Task AddRangedAsync(ICollection<ProductColorEntity> entities);
    }
}
