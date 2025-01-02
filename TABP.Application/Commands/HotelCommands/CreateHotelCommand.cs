using TABP.Application.DTOs.HotelDtos;
using MediatR;

namespace Application.Commands.HotelCommands;

public class CreateHotelCommand : IRequest<HotelNoRoomsDto?>
{
    public Guid CityId { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public float Rating { get; set; }
    public string StreetAddress { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public int FloorsNumber { get; set; }
}