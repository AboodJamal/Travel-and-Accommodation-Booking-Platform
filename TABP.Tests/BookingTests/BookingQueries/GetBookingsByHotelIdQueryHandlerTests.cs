using Application.Handlers.BookingHandlers;
using Application.Queries.BookingQueries;
using AutoMapper;
using Infrastructure.ExtraModels;
using Infrastructure.Interfaces;
using Moq;
using TABP.Application.DTOs.BookingDtos;
using TABP.Domain.Entities;


namespace TABP.Tests.BookingTests.BookingQueries
{
    public class GetBookingsByHotelIdQueryHandlerTests
    {
        private readonly Mock<BookingRepositoryInterface> _mockBookingRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetBookingsByHotelIdQueryHandler _handler;

        public GetBookingsByHotelIdQueryHandlerTests()
        {
            _mockBookingRepository = new Mock<BookingRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetBookingsByHotelIdQueryHandler(_mockBookingRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsPaginatedList()
        {
            // Arrange
            var query = new GetBookingsByHotelIdQuery
            {
                HotelId = Guid.NewGuid(),
                PageNumber = 1,
                PageSize = 10
            };

            var bookings = new List<Booking>
        {
            new Booking { Id = Guid.NewGuid() },
            new Booking { Id = Guid.NewGuid() }
        };

            var paginatedList = new PaginatedList<Booking>(bookings, new PageData(2, 1, 10));

            _mockBookingRepository
                .Setup(repo => repo.GetAllByHotelIdAsync(query.HotelId, query.PageNumber, query.PageSize))
                .ReturnsAsync(paginatedList);

            _mockMapper
                .Setup(mapper => mapper.Map<List<BookingDto>>(bookings))
                .Returns(bookings.Select(b => new BookingDto { Id = b.Id }).ToList());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count);
            _mockBookingRepository.Verify(repo => repo.GetAllByHotelIdAsync(query.HotelId, query.PageNumber, query.PageSize), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<BookingDto>>(bookings), Times.Once);
        }
    }
}
