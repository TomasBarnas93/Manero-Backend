using Manero_Backend.Models.Dtos.Order;

namespace Manero_Backend.Models.Entities
{
    public class OrderStatusEntity : BaseEntity
    {
        public long CompletedUnix { get; set; }
        public long EstimatedTimeUnix { get; set; }
        public bool Completed { get; set; }

        public Guid OrderStatusTypeId { get; set; }
        public OrderStatusTypeEntity OrderStatusType { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }


        public static implicit operator OrderStatusDto(OrderStatusEntity orderStatusEntity)
        {
            return new OrderStatusDto()
            {
                Name = orderStatusEntity.OrderStatusType.Name,
                Description = orderStatusEntity.Completed ? orderStatusEntity.OrderStatusType.DescriptionCompleted + DateTimeOffset.FromUnixTimeSeconds(orderStatusEntity.CompletedUnix) : orderStatusEntity.OrderStatusType.DescriptionEstimated + DateTimeOffset.FromUnixTimeSeconds(orderStatusEntity.EstimatedTimeUnix),
                Completed = orderStatusEntity.Completed
            };
        }
    }
}
