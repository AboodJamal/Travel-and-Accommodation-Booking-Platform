using Application.Commands.ReviewCommands;
using TABP.Application.DTOs.ReviewsDtos;
using Application.Queries.ReviewQueries;
using AutoMapper;
using TABP.Domain.Entities;

namespace TABP.Application.ApplicationServices.ProfileMappers;

public class ReviewProfileMapper : Profile
{
    public ReviewProfileMapper()
    {
        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewCreationDto, CreateReviewCommand>();
        CreateMap<CreateReviewCommand, Review>();
        CreateMap<ReviewQueryDto, GetReviewsQuery>();
    }
}