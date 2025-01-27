using TABP.Application.DTOs.RoomAmenityDtos;
using FluentValidation;
using TABP.API.Extra;

namespace TAABP.API.Features.RoomAmenity.RoomAmenityValidators;

public class UpdateRoomAmenityValidator : GenericValidator<RoomAmenityUpdateDto>
{
    public UpdateRoomAmenityValidator()
    {
        RuleFor(roomAmenity => roomAmenity.Name)
            .NotEmpty()
            .WithMessage("Name field shouldn't be empty");

        RuleFor(roomAmenity => roomAmenity.Description)
            .NotEmpty()
            .WithMessage("Description field shouldn't be empty");
    }
}