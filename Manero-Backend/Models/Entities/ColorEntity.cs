using System.ComponentModel.DataAnnotations;

namespace Manero_Backend.Models.Entities
{
    public class ColorEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<OrderProductEntity> OrderProducts { get; set; }
    }
}
