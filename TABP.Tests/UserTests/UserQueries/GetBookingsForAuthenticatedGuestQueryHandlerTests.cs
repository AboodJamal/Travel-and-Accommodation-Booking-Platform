

using Application.Handlers.UserHandlers;
using Application.Queries.UserQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Moq;
using TABP.Application.DTOs.BookingDtos;
using TABP.Domain.Entities;

namespace TABP.Tests.UserTests.UserQueries
{
    public class GetBookingsForAuthenticatedGuestQueryHandlerTests
    {
        private readonly Mock<UserRepositoryInterface> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetBookingsForAuthenticatedGuestQueryHandler _handler;

        public GetBookingsForAuthenticatedGuestQueryHandlerTests()
        {
            _mockUserRepository = new Mock<UserRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetBookingsForAuthenticatedGuestQueryHandler(_mockUserRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsBookings()
        {
            // Arrange
            var query = new GetBookingsForAuthenticatedGuestQuery
            {
                Email = "guest@example.com",
                Count = 5
            };

            var bookings = new List<Booking>
        {
            new Booking { Id = Guid.NewGuid() },
            new Booking { Id = Guid.NewGuid() }
        };

            var bookingDtos = bookings.Select(b => new BookingDto { Id = b.Id }).ToList();

            _mockUserRepository
                .Setup(repo => repo.GetBookingsForSpecificAuthenticatedGuestAsync(query.Email, query.Count))
                .ReturnsAsync(bookings);

            _mockMapper
                .Setup(mapper => mapper.Map<List<BookingDto>>(bookings))
                .Returns(bookingDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockUserRepository.Verify(repo => repo.GetBookingsForSpecificAuthenticatedGuestAsync(query.Email, query.Count), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<BookingDto>>(bookings), Times.Once);
        }
    }
}
