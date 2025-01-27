using System.Text.Json;
using Application.Commands.CityCommands;
using TABP.Application.DTOs.CityDtos;
using Application.Queries.CityQueries;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Exceptions;
using Infrastructure.ImageServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Extra;
using TABP.API.Features.City.CityValidators;

namespace TABP.API.Features.City;

[ApiController]
[ApiVersion("1")]
[Route("api/cities")]
public class CitiyController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ImageServiceInterface _imageService;

    public CitiyController(IMediator mediator,
        IMapper mapper,
        ImageServiceInterface imageService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _imageService = imageService;
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

    /// <summary>
    /// Updates an existing city with the provided data.
    /// </summary>
    /// <param name="cityId">The unique identifier for the city.</param>
    /// <param name="cityForUpdateDto">The data for updating the city.</param>
    /// <returns>Indicates successful update.</returns>
    [HttpPut("{cityId:guid}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize("IsAdmin")]
    public async Task<ActionResult> UpdateCityAsync(Guid cityId, CityUpdateDto cityForUpdateDto)
    {
        try
        {
            var validator = new UpdateCityValidator();
            var errors = await validator.CheckForValidationErrorsAsync(cityForUpdateDto);
            if (errors.Count > 0) return BadRequest(errors);

            var request = _mapper.Map<UpdateCityCommand>(cityForUpdateDto);
            request.Id = cityId;
            await _mediator.Send(request);
            return Ok(new { Message = "City updated successfully." });
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

    /// <summary>
    /// Partially updates an existing city based on a JSON Patch document.
    /// </summary>
    /// <param name="cityId">The unique identifier for the city.</param>
    /// <param name="patchDocument">JSON Patch document for partial update.</param>
    /// <returns>Indicates successful partial update.</returns>
    [HttpPatch("{cityId:guid}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize("IsAdmin")]
    public async Task<ActionResult> PartialUpdateCityAsync(Guid cityId,
    JsonPatchDocument<CityUpdateDto> patchDocument)
    {
        try
        {
            var cityDto = await _mediator.Send(new GetCityByIdQuery { Id = cityId });
            var cityForUpdateDto = _mapper.Map<CityUpdateDto>(cityDto);
            patchDocument.ApplyTo(cityForUpdateDto, ModelState);

            if (!ModelState.IsValid) return BadRequest();
            if (!TryValidateModel(cityForUpdateDto)) return BadRequest();

            var request = _mapper.Map<UpdateCityCommand>(cityForUpdateDto);
            request.Id = cityId;

            await _mediator.Send(request);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConstraintViolationException e)
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Uploads an image for a city based on its unique identifier (GUID).
    /// </summary>
    /// <param name="cityId">The unique identifier of the city.</param>
    /// <param name="file">The image file to be uploaded.</param>
    /// <returns>
    /// - 200 OK: If the image is uploaded successfully.
    /// - 404 Not Found: If the city with the given ID does not exist.
    /// - 400 Bad Request: If the image format is not supported.
    /// - 500 Internal Server Error: If an unexpected error occurs during the operation.
    /// </returns>
    [HttpPost("{cityId:guid}/gallery")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize("IsAdmin")]
    public async Task<IActionResult> UploadImageForCityAsync(Guid cityId, IFormFile file)
    {
        try
        {

            if (_mediator == null) return BadRequest("Mediator service is not available.");
            if (_imageService == null) return BadRequest("Image service is not available.");
            if (file == null) return BadRequest("No file uploaded.");

            bool cityExists = await _mediator.Send(new CheckIfCityExistsQuery { Id = cityId });
            if (!cityExists)
                return NotFound($"City with ID {cityId} does not exist");

            var imageCreationDto = await ImageUploadHelper.CreateImageCreationDto(cityId, file, ImgType.Gallery);

            await _imageService.UploadImageAsync(imageCreationDto);
            return Ok("Image uploaded successfully.");
        }
        catch (NotSupportedException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Retrieves all photos for a city based on its unique identifier (GUID).
    /// </summary>
    /// <param name="cityId">The unique identifier of the city.</param>
    /// <returns>
    /// - 200 OK: Returns the list of photos for the specified city.
    /// - 404 Not Found: If the city with the given ID does not exist.
    /// - 500 Internal Server Error: If an unexpected error occurs during the operation.
    /// </returns>
    [HttpGet("{cityId:guid}/photos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetAllCityPhotos(Guid cityId)
    {
        if (!await _mediator.Send(new CheckIfCityExistsQuery { Id = cityId }))
            return NotFound($"City with ID = {cityId} does not exist");

        var images = await _imageService.GetAllImagesAsync(cityId);
        return Ok(images);
    }

    /// <summary>
    /// Uploads a thumbnail for a city based on its unique identifier (GUID).
    /// </summary>
    /// <param name="cityId">The unique identifier of the city.</param>
    /// <param name="file">The image file to be uploaded.</param>
    /// <returns>
    /// - 200 OK: If the thumbnail is uploaded successfully.
    /// - 404 Not Found: If the city with the given ID does not exist.
    /// - 400 Bad Request: If the thumbnail format is not supported.
    /// - 500 Internal Server Error: If an unexpected error occurs during the operation.
    /// </returns>
    [HttpPut("{cityId:guid}/thumbnail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize("IsAdmin")]
    public async Task<IActionResult> UploadThumbnailForCityAsync(Guid cityId, IFormFile file)
    {
        try
        {
            if (!await _mediator.Send(new CheckIfCityExistsQuery { Id = cityId }))
                return NotFound($"City with ID {cityId} does not exist");

            var imageCreationDto = await ImageUploadHelper
            .CreateImageCreationDto(cityId, file, ImgType.Thumbnail);
            await _imageService.UploadThumbnailAsync(imageCreationDto);
            return Ok("Thumbnail uploaded successfully.");
        }
        catch (NotSupportedException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Deletes an image associated with a city.
    /// </summary>
    /// <param name="cityId">The unique identifier of the city.</param>
    /// <param name="photoId">The unique identifier of the photo to be deleted.</param>
    /// <returns>
    /// - 204 No Content: If the image is deleted successfully.
    /// - 404 Not Found: If the specified city or photo does not exist.
    /// - 500 Internal Server Error: If an unexpected error occurs.
    /// </returns>
    [HttpDelete("{cityId:guid}/photos/{photoId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = "IsAdmin")]
    public async Task<IActionResult> DeleteCityImageAsync(Guid cityId, Guid photoId)
    {
        try
        {
            await _imageService.DeleteImageAsync(cityId, photoId);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message + $" for City  ID {cityId}.");
        }
        catch (InvalidOperationException e)
        {
            return StatusCode(500, new
            {
                error = "Server Error",
                message = e.Message
            });
        }
    }

}