namespace Manero_Backend.Models.Dtos.Review;

public class ReviewResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public int StarRating { get; set; }
    public string? Description { get; set; }
}