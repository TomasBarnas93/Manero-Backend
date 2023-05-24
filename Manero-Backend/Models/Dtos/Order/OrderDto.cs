using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Dtos.Order
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public long LatestCompletedUnix { get; set; }
        public Guid OrderStatusTypeId { get; set; }
    }
}
