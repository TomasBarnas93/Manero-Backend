using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Schemas.Product;

namespace Manero_Backend.Models.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        public Task FillDataAsync();

        public Task<List<ProductEntity>> GetByOption(ProductOptionSchema option);
    }
}
