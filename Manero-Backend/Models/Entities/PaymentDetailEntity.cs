using Manero_Backend.Models.Auth;

namespace Manero_Backend.Models.Entities
{
    public class PaymentDetailEntity : BaseEntity
    {
        public string CardName { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string Cvv { get; set; } = null!;
        public string ExpDate { get; set; } = null!;

        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public ICollection<OrderEntity> Orders { get; set; }

    }
}
