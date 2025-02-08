using Application.Handlers.UserHandlers;
using Application.Queries.UserQueries;
using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Moq;
using TABP.Application.DTOs.HotelDtos;
using TABP.Domain.Entities;

namespace TABP.Tests.UserTests.UserQueries
{
    public class GetRecentlyVisitedHotelsForGuestQueryHandlerTests
    {
        private readonly Mock<UserRepositoryInterface> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetRecentlyVisitedHotelsForGuestQueryHandler _handler;

        public GetRecentlyVisitedHotelsForGuestQueryHandlerTests()
        {
            _mockUserRepository = new Mock<UserRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetRecentlyVisitedHotelsForGuestQueryHandler(_mockUserRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsHotels()
        {
            // Arrange
            var query = new GetRecentlyVisitedHotelsForGuestQuery
            {
                GuestId = Guid.NewGuid(),
                Count = 5
            };

            var hotels = new List<Hotel>
        {
            new Hotel { Id = Guid.NewGuid() },
            new Hotel { Id = Guid.NewGuid() }
        };

            var hotelDtos = hotels.Select(h => new HotelNoRoomsDto { Id = h.Id }).ToList();

            _mockUserRepository
                .Setup(repo => repo.IsExistAsync(query.GuestId))
                .ReturnsAsync(true);

            _mockUserRepository
                .Setup(repo => repo.GetRecentlyVisitedHotelsForSpecificGuestAsync(query.GuestId, query.Count))
                .ReturnsAsync(hotels);

            _mockMapper
                .Setup(mapper => mapper.Map<List<HotelNoRoomsDto>>(hotels))
                .Returns(hotelDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockUserRepository.Verify(repo => repo.IsExistAsync(query.GuestId), Times.Once);
            _mockUserRepository.Verify(repo => repo.GetRecentlyVisitedHotelsForSpecificGuestAsync(query.GuestId, query.Count), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<HotelNoRoomsDto>>(hotels), Times.Once);
        }

        [Fact]
        public async Task Handle_GuestNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var query = new GetRecentlyVisitedHotelsForGuestQuery
            {
                GuestId = Guid.NewGuid(),
                Count = 5
            };

            _mockUserRepository
                .Setup(repo => repo.IsExistAsync(query.GuestId))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None)
            );

            _mockUserRepository.Verify(repo => repo.IsExistAsync(query.GuestId), Times.Once);
            _mockUserRepository.Verify(repo => repo.GetRecentlyVisitedHotelsForSpecificGuestAsync(It.IsAny<Guid>(), It.IsAny<int>()), Times.Never);
            _mockMapper.Verify(mapper => mapper.Map<List<HotelNoRoomsDto>>(It.IsAny<List<Hotel>>()), Times.Never);
        }
    }

}
