namespace Manero_Backend.Models.Dtos.Review;

public class ReviewRequest
{
    public string UserName { get; set; } = null!;
    public int StarRating { get; set; }
    public string? Description { get; set; } 
}