namespace Manero_Backend.Models.Entities
{
    public class TagEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        
        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
