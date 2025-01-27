using System.Text.Json;
using Application.Commands.RoomAmenityCommands;
using TABP.Application.DTOs.RoomAmenityDtos;
using Application.Queries.RoomAmenityQueries;
using AutoMapper;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TAABP.API.Features.RoomAmenity.RoomAmenityValidators;

namespace TAABP.API.Features.RoomAmenity;

[ApiController]
[Route("api/roomAmenities")]
[ApiVersion("1.0")]
public class RoomAmenityController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public RoomAmenityController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Retrieves a paginated list of room amenities based on the specified search criteria.
    /// </summary>
    /// <param name="getAllRoomAmenitiesQuery">Query parameters for retrieving room amenities.</param>
    /// <returns>Returns a paginated list of room amenities.</returns>
    /// <remarks>
    /// This endpoint supports pagination to retrieve a subset of room amenities based on the provided search.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetAllRoomAmenitiesAsync(
        [FromQuery] GetAllRoomAmenitiesQuery getAllRoomAmenitiesQuery)
    {
        var validator = new GetAllRoomAmenitiesValidator();
        var errors = await validator.CheckForValidationErrorsAsync(getAllRoomAmenitiesQuery);
        if (errors.Count > 0) return BadRequest(errors);
        
        var paginatedListOfAmenities = await _mediator.Send(getAllRoomAmenitiesQuery);
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginatedListOfAmenities.PageData));

        return Ok(paginatedListOfAmenities.Items);
    }
    
    /// <summary>
    /// Retrieves details for a specific room amenity.
    /// </summary>
    /// <param name="roomAmenityId">The unique identifier for the room amenity.</param>
    /// <returns>Returns the room amenity details.</returns>
    [HttpGet("{roomAmenityId:guid}", Name = "GetRoomAmenity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetRoomAmenityAsync(Guid roomAmenityId)
    {
        var request = new GetRoomAmenityByIdQuery {Id = roomAmenityId};
        var result = await _mediator.Send(request);
        if (result is null) return NotFound();
        return Ok(result);
    }
    
    /// <summary>
    /// Creates a new room amenity based on the provided data.
    /// </summary>
    /// <param name="roomAmenity">The data for creating a new room amenity.</param>
    /// <returns>Returns the created room amenity details.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize("IsAdmin")]
    public async Task<ActionResult<RoomAmenityDto>> 
        CreateRoomAmenityAsync(RoomAmenityCreationDto roomAmenity)
    {
        var validator = new CreateRoomAmenityValidator();
        var errors = await validator.CheckForValidationErrorsAsync(roomAmenity);
        if (errors.Count > 0) return BadRequest(errors);
        
        var request = _mapper.Map<CreateRoomAmenityCommand>(roomAmenity);
        var amenityToReturn = await _mediator.Send(request);
        if (amenityToReturn is null)
            return BadRequest();
        
        return CreatedAtRoute("GetRoomAmenity",
            new {
                roomAmenityId = amenityToReturn.Id
            },amenityToReturn);
    }
    
    /// <summary>
    /// Deletes a room amenity with the specified ID.
    /// </summary>
    /// <param name="roomAmenityId">The unique identifier for the room amenity.</param>
    /// <returns>Indicates successful deletion.</returns>
    [HttpDelete("{roomAmenityId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize("IsAdmin")]
    public async Task<ActionResult> DeleteRoomAmenityAsync(Guid roomAmenityId)
    {
        try
        {
            var deleteRoomAmenityCommand = new DeleteRoomAmenityCommand { Id = roomAmenityId };
            await _mediator.Send(deleteRoomAmenityCommand);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Updates an existing room amenity with the provided data.
    /// </summary>
    /// <param name="roomAmenityId">The unique identifier for the room amenity.</param>
    /// <param name="roomAmenityForUpdateDto">The data for updating the room amenity.</param>
    /// <returns>Indicates successful update.</returns>
    [HttpPut("{roomAmenityId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize("IsAdmin")]
    public async Task<ActionResult> UpdateRoomAmenityAsync(Guid roomAmenityId,
    RoomAmenityUpdateDto roomAmenityForUpdateDto)
    {
        try
        {
            var validator = new UpdateRoomAmenityValidator();
            var errors = await validator.CheckForValidationErrorsAsync(roomAmenityForUpdateDto);
            if (errors.Count > 0) return BadRequest(errors);
        
            var updateCommand = _mapper.Map<UpdateRoomAmenityCommand>(roomAmenityForUpdateDto);
            updateCommand.Id = roomAmenityId;
            await _mediator.Send(updateCommand);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (ConstraintViolationException e)
        {
            return BadRequest(e.Message);
        }
    }

}