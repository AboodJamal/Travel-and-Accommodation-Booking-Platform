using System.Security.Claims;
using System.Text.Json;
using Application.Commands.ReviewCommands;
using TABP.Application.DTOs.ReviewsDtos;
using Application.Queries.BookingQueries;
using Application.Queries.ReviewQueries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TAABP.API.Features.Review.ReviewValidators;
using TABP.Domain.Entities;


namespace TAABP.API.Features.Review;

[ApiController]
[Route("/api/reviews")]
[ApiVersion("1.0")]
public class ReviewController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ReviewController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Retrieves a paginated list of reviews for a specific hotel.
    /// </summary>
    /// <param name="hotelId">The ID of the hotel for which reviews are requested.</param>
    /// <param name="reviewQueryDto">DTO containing parameters for pagination and filtering.</param>
    /// <returns>
    /// Returns a paginated list of reviews for the specified hotel.
    /// </returns>
    /// <response code="200">Returns a paginated list of reviews.</response>
    /// <response code="400">Returns validation errors if the request parameters are invalid.</response>
    /// <response code="401">Returns if the user is not authenticated.</response>
    /// <response code="500">Returns if an unexpected error occurs.</response>
    [HttpGet("hotels/{hotelId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetAllReviewsAsync(Guid hotelId,
        [FromQuery] ReviewQueryDto reviewQueryDto)
    {
        var reviewQuery = _mapper.Map<GetReviewsQuery>(reviewQueryDto);
        reviewQuery.HotelId = hotelId;
        
        var validator = new ReviewsQueryValidator();
        var errors = await validator.CheckForValidationErrorsAsync(reviewQuery);
        if (errors.Count > 0) return BadRequest(errors);
        
        var paginatedListOfCities = await _mediator.Send(reviewQuery);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginatedListOfCities.PageData));

        return Ok(paginatedListOfCities.Items);
    }
    
    /// <summary>
    /// Creates a new review.
    /// </summary>
    /// <param name="review">DTO containing review data.</param>
    /// <returns>
    /// Returns the created review if successful.
    /// </returns>
    /// <response code="201">Returns the created review.</response>
    /// <response code="400">Invalid parameters or existing review for the booking.</response>
    /// <response code="404">Returns if the booking does not exist.</response>
    /// <response code="500">Returns if an unexpected error occurs.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult<ReviewDto>> CreateReviewAsync(ReviewCreationDto review)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity; 
        var authEmail = identity.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;


        if (!await CheckIfAuthenticatedUserIsBookerAsync(review.BookingId, authEmail))
            return Unauthorized("The authenticated user must be the one who booked");

        if (await _mediator.Send(new CheckIfBookingExistsQuery{Id = review.BookingId}))
            return NotFound($"Booking with ID does not exist");
        
        if(await _mediator.Send(new CheckIfReviewExistsForBookingQuery{BookingId = review.BookingId}))
            return Conflict("You already made a review for this booking");
        
        var validator = new CreateReviewValidator();
        var errors = await validator.CheckForValidationErrorsAsync(review);
        if (errors.Count > 0) return BadRequest(errors);
        
        var request = _mapper.Map<CreateReviewCommand>(review);
        var reviewToReturn = await _mediator.Send(request);
        if (reviewToReturn is null)
        {
            return BadRequest();
        }
        return Ok("Review submitted successfully!");
    }
    
    private async Task<bool> CheckIfAuthenticatedUserIsBookerAsync(Guid bookingId, string? guestEmail)
    {
        return await _mediator.Send(new CheckIfBookingExistsForGuestQuery
        {
            BookingId = bookingId,
            GuestEmail = guestEmail
        });
    }
   
}