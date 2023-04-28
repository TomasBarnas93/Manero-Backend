using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Dtos
{
    public class ProductResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Color { get; set; } = null!;
		public string Size { get; set; } = null!;
		public decimal Price { get; set; }
		public int StarRating { get; set; }
		public string? ImageUrl { get; set; }
		public TagEntity Tag { get; set; } = null!;
		public CategoryEntity Category { get; set; } = null!;
	}
}
