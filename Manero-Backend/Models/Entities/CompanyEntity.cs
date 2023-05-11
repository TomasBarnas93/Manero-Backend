namespace Manero_Backend.Models.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<PromoCodeEntity> PromoCodes { get; set; } //M:1
        public ICollection<ProductEntity> Products { get; set; } //M:1
    }
}
