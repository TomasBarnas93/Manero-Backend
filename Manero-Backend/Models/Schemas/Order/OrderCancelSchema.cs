namespace Manero_Backend.Models.Schemas.Order
{
    public class OrderCancelSchema
    {
        public Guid OrderId { get; set; }
        public string CancelMessage { get; set; } = null!;
    }
}
