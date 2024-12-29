namespace TABP.Application.DTOs.RoomDtos;

public record RoomCreationDto
{
    public Guid RoomTypeId { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public string RoomView { get; set; }
    public float RoomRating { get; set; }
}