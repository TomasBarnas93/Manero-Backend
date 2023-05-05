using Manero_Backend.Models.Auth;

namespace Manero_Backend.Models.Entities
{
	public class ReviewEntity : BaseEntity
	{
		public int StarRating { get; set; }
		public string? Description { get; set; }
		
		public string AppUserId { get; set; } = null!;
		public AppUser AppUser { get; set; } = null!;
		
		public Guid ProductId { get; set; }
		public ProductEntity Product { get; set; } = new ProductEntity();
	}
}
