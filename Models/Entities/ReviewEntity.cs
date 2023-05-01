﻿namespace Manero_Backend.Models.Entities
{
	public class ReviewEntity
	{
		public int Id { get; set; }
		public string UserName { get; set; } = null!;
		public int StarRating { get; set; }
		public string? Description { get; set; }

		public Guid ProductId { get; set; }
		public ProductEntity Product { get; set; } = new ProductEntity();
	}
}