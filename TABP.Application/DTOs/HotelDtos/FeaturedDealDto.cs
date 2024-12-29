namespace TABP.Application.DTOs.HotelDtos;

public record FeaturedDealDto
{
    public string CityName { get; set; }
    public Guid HotelId { get; set; }
    public string HotelName { get; set; }
    public float HotelRating { get; set; }
    public Guid ClassId { get; set; }
    public float RoomPrice { get; set; }
    public float DiscountPercentage { get; set; }
    public float RoomFinalPrice { get; set; }
}