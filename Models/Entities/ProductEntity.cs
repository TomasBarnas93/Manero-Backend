using Manero_Backend.Models.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manero_Backend.Models.Entities;

public class ProductEntity
{
	[Key]
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string Color { get; set; } = null!;
	public string Size { get; set; } = null!;

	[Required]
	[Column(TypeName = "money")]
	public decimal Price { get; set; }
	public int StarRating { get; set; }
	public string? ImageUrl { get; set; }
	public TagEntity Tag { get; set; } = null!;
	public CategoryEntity Category { get; set; } = null!;


	public static implicit operator ProductResponse(ProductEntity entity)
	{
		return new ProductResponse
		{
			Id = entity.Id,
			Name = entity.Name,
			Description = entity.Description,
			Color = entity.Color,
			Size = entity.Size,
			Price = entity.Price,
			StarRating = entity.StarRating,
			ImageUrl = entity.ImageUrl,
            Tag = new TagEntity { Name = entity.Tag.Name },
            Category = new CategoryEntity { Name = entity.Category.Name }
        };
	}
}
