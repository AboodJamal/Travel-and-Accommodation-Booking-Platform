namespace TABP.Application.DTOs.ReviewsDtos;

public record ReviewCreationDto
{
    public Guid BookingId { get; set; }
    public string ReviewComment { get; set; }
    public float ReviewRating { get; set; } = -1;
}