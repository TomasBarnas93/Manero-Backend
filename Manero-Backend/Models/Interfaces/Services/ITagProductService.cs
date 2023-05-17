using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services
{
    public interface ITagProductService
    {
        public Task AddRangedAsync(ICollection<TagProductEntity> entities);
    }
}
