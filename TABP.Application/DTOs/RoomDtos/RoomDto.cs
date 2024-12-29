namespace TABP.Application.DTOs.RoomDtos;

public record RoomDto
{
    public Guid Id { get; set; }
    public Guid RoomTypeId { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public string RoomView { get; set; }
    public float RoomRating { get; set; }
}