using System.Text.Json;
using Application.Commands.CityCommands;
using TABP.Application.DTOs.CityDtos;
using Application.Queries.CityQueries;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Exceptions;
//using Infrastructure.ImageStorage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
//using TAABP.API.Utils;
using TABP.API.Features.City.CityValidators;

namespace TABP.API.Features.City;

[ApiController]
[ApiVersion("1")]
[Route("api/cities")]
public class CitiesController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    //private readonly IImageService _imageService;

    public CitiesController(IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a paginated list of cities, optionally including hotel details,
    /// based on the specified criteria.
    /// </summary>
    /// <param name="cityQuery">Query parameters for retrieving cities.</param>
    /// <returns>Returns a paginated list of cities (with or without hotel details)
    /// based on the query criteria.</returns>
    /// <remarks>
    /// This endpoint supports pagination and allows filtering cities based on the provided search criteria.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = "IsAdmin")]
    public async Task<IActionResult> GetAllCitiesAsync(
    [FromQuery] GetCitiesQuery cityQuery)
    {
        var validator = new CitiesQueryValidator();
        var errors = await validator.CheckForValidationErrorsAsync(cityQuery);
        if (errors.Count > 0) return BadRequest(errors);

        var paginatedListOfCities = await _mediator.Send(cityQuery);
        Response.Headers.Append("X-Pagination",
            JsonSerializer.Serialize(paginatedListOfCities.PageData));

        return cityQuery.IncludeHotels ?
               Ok(paginatedListOfCities.Items) :
               Ok(_mapper
                 .Map<List<CityNoHotelsDto>>
                 (paginatedListOfCities.Items));
    }

    /// <summary>
    /// Retrieves details for a specific city, with optional hotel details.
    /// </summary>
    /// <param name="cityId">The unique identifier for the city.</param>
    /// <param name="includeHotels">Include hotel details in the response.</param>
    /// <returns>Returns the city details (with or without hotel details).</returns>
    [HttpGet("{cityId:guid}", Name = "GetCity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetCityAsync(Guid cityId, bool includeHotels = false)
    {
        var request = new GetCityByIdQuery { Id = cityId, IncludeHotels = includeHotels };
        var result = await _mediator.Send(request);
        if (result is null) return NotFound();
        return includeHotels ? Ok(result) : Ok(_mapper.Map<CityNoHotelsDto>(result));
    }

    /// <summary>
    /// Creates a new city based on the provided data.
    /// </summary>
    /// <param name="city">The data for creating a new city.</param>
    /// <returns>Returns the created city details.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize("IsAdmin")]
    public async Task<ActionResult<CityNoHotelsDto>> CreateCityAsync(CityCreationDto city)
    {
        var validator = new CreateCityValidator();
        var errors = await validator.CheckForValidationErrorsAsync(city);
        if (errors.Count > 0) return BadRequest(errors);

        var request = _mapper.Map<CreateCityCommand>(city);
        var cityToReturn = await _mediator.Send(request);
        if (cityToReturn is null)
        {
            return BadRequest();
        }
        return CreatedAtRoute("GetCity",
        new
        {
            cityId = cityToReturn.Id
        }, cityToReturn);
    }

    /// <summary>
    /// Deletes a city with the specified ID.
    /// </summary>
    /// <param name="cityId">The unique identifier for the city.</param>
    /// <returns>Indicates successful deletion.</returns>
    [HttpDelete("{cityId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize("IsAdmin")]
    public async Task<ActionResult> DeleteCityAsync(Guid cityId)
    {
        var deleteCityCommand = new DeleteCityCommand { Id = cityId };
        try
        {
            await _mediator.Send(deleteCityCommand);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }
}