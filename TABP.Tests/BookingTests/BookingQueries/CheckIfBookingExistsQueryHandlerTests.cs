using Application.Handlers.BookingHandlers;
using Application.Queries.BookingQueries;
using Infrastructure.Interfaces;
using Moq;


namespace TABP.Tests.BookingTests.BookingQueries
{
    public class CheckIfBookingExistsQueryHandlerTests
    {
        private readonly Mock<BookingRepositoryInterface> _mockBookingRepository;
        private readonly CheckIfBookingExistsQueryHandler _handler;

        public CheckIfBookingExistsQueryHandlerTests()
        {
            _mockBookingRepository = new Mock<BookingRepositoryInterface>();
            _handler = new CheckIfBookingExistsQueryHandler(_mockBookingRepository.Object);
        }

        [Fact]
        public async Task Handle_BookingExists_ReturnsTrue()
        {
            // Arrange
            var query = new CheckIfBookingExistsQuery
            {
                Id = Guid.NewGuid()
            };

            _mockBookingRepository
                .Setup(repo => repo.IsExistAsync(query.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mockBookingRepository.Verify(repo => repo.IsExistAsync(query.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_BookingDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var query = new CheckIfBookingExistsQuery
            {
                Id = Guid.NewGuid()
            };

            _mockBookingRepository
                .Setup(repo => repo.IsExistAsync(query.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mockBookingRepository.Verify(repo => repo.IsExistAsync(query.Id), Times.Once);
        }
    }
}
