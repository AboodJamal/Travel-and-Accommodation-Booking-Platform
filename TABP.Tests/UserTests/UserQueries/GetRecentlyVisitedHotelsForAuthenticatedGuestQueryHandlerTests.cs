using Application.Handlers.UserHandlers;
using Application.Queries.UserQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Moq;
using TABP.Application.DTOs.HotelDtos;
using TABP.Domain.Entities;

namespace TABP.Tests.UserTests.UserQueries
{
    public class GetRecentlyVisitedHotelsForAuthenticatedGuestQueryHandlerTests
    {
        private readonly Mock<UserRepositoryInterface> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetRecentlyVisitedHotelsForAuthenticatedGuestQueryHandler _handler;

        public GetRecentlyVisitedHotelsForAuthenticatedGuestQueryHandlerTests()
        {
            _mockUserRepository = new Mock<UserRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetRecentlyVisitedHotelsForAuthenticatedGuestQueryHandler(_mockUserRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsHotels()
        {
            // Arrange
            var query = new GetRecentlyVisitedHotelsForAuthenticatedGuestQuery
            {
                Email = "guest@example.com",
                Count = 5
            };

            var hotels = new List<Hotel>
        {
            new Hotel { Id = Guid.NewGuid() },
            new Hotel { Id = Guid.NewGuid() }
        };

            var hotelDtos = hotels.Select(h => new HotelNoRoomsDto { Id = h.Id }).ToList();

            _mockUserRepository
                .Setup(repo => repo.GetRecentlyVisitedHotelsForSpecificAuthenticatedGuestAsync(query.Email, query.Count))
                .ReturnsAsync(hotels);

            _mockMapper
                .Setup(mapper => mapper.Map<List<HotelNoRoomsDto>>(hotels))
                .Returns(hotelDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockUserRepository.Verify(repo => repo.GetRecentlyVisitedHotelsForSpecificAuthenticatedGuestAsync(query.Email, query.Count), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<HotelNoRoomsDto>>(hotels), Times.Once);
        }
    }
}
