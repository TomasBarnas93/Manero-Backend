namespace Manero_Backend.Models.Dtos.Order
{
    public class OrderStatusDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Completed { get; set; }
    }
}
