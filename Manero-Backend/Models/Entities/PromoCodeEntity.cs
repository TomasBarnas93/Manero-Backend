namespace Manero_Backend.Models.Entities
{
    public class PromoCodeEntity : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public CompanyEntity Company { get; set; }
        public decimal Discount { get; set; }
        public long ValidToUnix { get; set; }
        public string Code { get; set; } = null!;

        public ICollection<UserPromoCodeEntity> UserPromoCodes { get; set; } //M:M
        public ICollection<OrderEntity> Orders { get; set; } //M:1
    }
}
