namespace Manero_Backend.Models.Entities
{
    public class OrderStatusEntity : BaseEntity
    {
        public long EstTimeUnix { get; set; }
        public Guid OrderStatusTypeId { get; set; }
        public OrderStatusTypeEntity OrderStatusType { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }
    }
}
