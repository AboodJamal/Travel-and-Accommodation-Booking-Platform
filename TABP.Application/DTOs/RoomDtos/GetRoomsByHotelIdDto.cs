namespace TABP.Application.DTOs.RoomDtos;

public record GetRoomsByHotelIdDto
{
    public bool WithAmenities { get; set; } = false;
    public string? QuerySearch { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}