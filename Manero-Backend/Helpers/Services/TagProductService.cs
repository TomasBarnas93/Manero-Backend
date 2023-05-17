using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;

namespace Manero_Backend.Helpers.Services
{
    public class TagProductService : ITagProductService
    {
        private readonly ITagProductRepository _tagProductRepository;

        public TagProductService(ITagProductRepository tagProductRepository)
        {
            _tagProductRepository = tagProductRepository;
        }

        public async Task AddRangedAsync(ICollection<TagProductEntity> entities)
        {
            await _tagProductRepository.AddRangedAsync(entities);
        }
    }
}
