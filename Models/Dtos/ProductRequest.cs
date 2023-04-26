using Manero_Backend.Helpers.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Dtos
{
	public class ProductRequest
	{
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Color { get; set; } = null!;
		public string Size { get; set; } = null!;
		public decimal Price { get; set; }
		public int StarRating { get; set; }
		public string? ImageUrl { get; set; }
		public ProductTagEnum Tag { get; set; }
		public ProductGenreEnum Genre { get; set; }

		public static implicit operator ProductEntity(ProductRequest product)
		{
			return new ProductEntity
			{
				Name = product.Name,
				Description = product.Description,
				Color = product.Color,
				Size = product.Size,
				Price = product.Price,
				StarRating = product.StarRating,
				ImageUrl = product.ImageUrl,
				Tag = product.Tag,
				Genre = product.Genre
			};
		}
	}
}
