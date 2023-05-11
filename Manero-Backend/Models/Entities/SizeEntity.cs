namespace Manero_Backend.Models.Entities
{
    public class SizeEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<OrderProductEntity> OrderProducts { get; set; }
    }
}
