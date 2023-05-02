using System.ComponentModel.DataAnnotations;

namespace Manero_Backend.Models.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        
        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
