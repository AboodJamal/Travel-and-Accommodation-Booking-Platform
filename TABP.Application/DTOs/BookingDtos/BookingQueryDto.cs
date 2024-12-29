namespace TABP.Application.DTOs.BookingDtos;

public record BookingQueryDto
{
    public string? QuerySearch { get; set; }
    public int PageNumber { get; set; } = 1; // def =1
    public int PageSize { get; set; } = 5; // de
}