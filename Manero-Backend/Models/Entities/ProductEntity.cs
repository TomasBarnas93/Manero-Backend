using System.ComponentModel.DataAnnotations.Schema;

namespace Manero_Backend.Models.Entities;

public class ProductEntity : BaseEntity
{
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	
	[Column(TypeName = "decimal(18,2)")]
	public decimal Price { get; set; }
	public string? ImageUrl { get; set; }

    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }

	public Guid CompanyId { get; set; }
	public CompanyEntity Company { get; set; }

	public ICollection<OrderProductEntity> OrderProducts { get; set; } //M:M
	public ICollection<TagProductEntity> TagProducts { get; set; } //M:M
	public ICollection<ReviewEntity> Reviews { get; set; } //M:M
	public ICollection<WishEntity> WishList { get; set; } //M:M
}
