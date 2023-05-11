namespace Manero_Backend.Models.Entities
{
    public class OrderStatusTypeEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<OrderStatusEntity> OrderStatuses { get; set; }
    }
}
