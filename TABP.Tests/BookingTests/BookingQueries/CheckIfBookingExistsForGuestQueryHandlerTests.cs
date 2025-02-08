using Application.Commands.BookingCommands;
using Application.Handlers.BookingHandlers;
using TABP.Domain.Entities;
using Infrastructure.Interfaces;
using AutoMapper;
using Moq;
using Xunit;
using TABP.Application.DTOs.BookingDtos;
using Application.Queries.BookingQueries;

namespace TABP.Tests.BookingTests.BookingQueries
{
    public class CheckIfBookingExistsForGuestQueryHandlerTests
    {
        private readonly Mock<BookingRepositoryInterface> _mockBookingRepository;
        private readonly CheckIfBookingExistsForGuestQueryHandler _handler;

        public CheckIfBookingExistsForGuestQueryHandlerTests()
        {
            _mockBookingRepository = new Mock<BookingRepositoryInterface>();
            _handler = new CheckIfBookingExistsForGuestQueryHandler(_mockBookingRepository.Object);
        }

        [Fact]
        public async Task Handle_BookingExistsForGuest_ReturnsTrue()
        {
            // Arrange
            var query = new CheckIfBookingExistsForGuestQuery
            {
                BookingId = Guid.NewGuid(),
                GuestEmail = "guest@example.com"
            };

            _mockBookingRepository
                .Setup(repo => repo.BookingExistsForGuestAsync(query.BookingId, query.GuestEmail))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mockBookingRepository.Verify(repo => repo.BookingExistsForGuestAsync(query.BookingId, query.GuestEmail), Times.Once);
        }

        [Fact]
        public async Task Handle_BookingDoesNotExistForGuest_ReturnsFalse()
        {
            // Arrange
            var query = new CheckIfBookingExistsForGuestQuery
            {
                BookingId = Guid.NewGuid(),
                GuestEmail = "guest@example.com"
            };

            _mockBookingRepository
                .Setup(repo => repo.BookingExistsForGuestAsync(query.BookingId, query.GuestEmail))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mockBookingRepository.Verify(repo => repo.BookingExistsForGuestAsync(query.BookingId, query.GuestEmail), Times.Once);
        }
    }
}
