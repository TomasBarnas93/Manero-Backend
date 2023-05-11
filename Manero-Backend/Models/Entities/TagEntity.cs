namespace Manero_Backend.Models.Entities
{
    public class TagEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        
        public ICollection<TagProductEntity> TagProducts { get; set; }
    }
}
