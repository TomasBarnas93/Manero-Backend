using Manero_Backend.Helpers.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
		public ProductTagEnum Tag { get; set; }
		public ProductGenreEnum Genre { get; set; }
	}
}
