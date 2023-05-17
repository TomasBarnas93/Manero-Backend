using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;

namespace Manero_Backend.Helpers.Services
{
    public class ProductColorService : IProductColorService
    {
        private readonly IProductColorRepository _productColorRepository;

        public ProductColorService(IProductColorRepository productColorRepository)
        {
            _productColorRepository = productColorRepository;
        }

        public async Task AddRangedAsync(ICollection<ProductColorEntity> entities)
        {
           await _productColorRepository.AddRangedAsync(entities);
        }
    }
}
