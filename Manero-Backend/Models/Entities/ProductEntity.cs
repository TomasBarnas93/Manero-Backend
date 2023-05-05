using System.ComponentModel.DataAnnotations.Schema;

namespace Manero_Backend.Models.Entities;

public class ProductEntity : BaseEntity
{
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string Color { get; set; } = null!;
	public string Size { get; set; } = null!;
	
	[Column(TypeName = "decimal(18,2)")]
	public decimal Price { get; set; }
	public int StarRating { get; set; }
	public string? ImageUrl { get; set; }
	public Guid TagId { get; set; }
	public TagEntity Tag { get; set; } = new TagEntity();
	public Guid CategoryId { get; set; }
	public CategoryEntity Category { get; set; } = new CategoryEntity();
	
	public ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
}
