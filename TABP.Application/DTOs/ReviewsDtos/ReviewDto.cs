namespace TABP.Application.DTOs.ReviewsDtos;

public record ReviewDto
{
    public Guid Id { get; set; }
    public string ReviewComment { get; set; }
    public DateTime ReviewDate { get; set; }
    public float ReviewRating { get; set; }
}