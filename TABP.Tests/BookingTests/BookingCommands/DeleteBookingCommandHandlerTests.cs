using Application.Commands.BookingCommands;
using Application.Handlers.BookingHandlers;
using TABP.Domain.Entities;
using Infrastructure.Interfaces;
using AutoMapper;
using Moq;
using Xunit;
using TABP.Application.DTOs.BookingDtos;

namespace TABP.Tests.BookingTests.BookingCommands
{
    public class DeleteBookingCommandHandlerTests
    {
        private readonly Mock<BookingRepositoryInterface> _mockBookingRepository;
        private readonly DeleteBookingCommandHandler _handler;

        public DeleteBookingCommandHandlerTests()
        {
            _mockBookingRepository = new Mock<BookingRepositoryInterface>();
            _handler = new DeleteBookingCommandHandler(_mockBookingRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_DeletesBooking()
        {
            // Arrange
            var command = new DeleteBookingCommand
            {
                Id = Guid.NewGuid()
            };

            _mockBookingRepository
                .Setup(repo => repo.DeleteAsync(command.Id))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockBookingRepository.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidBookingId_ThrowsException()
        {
            // Arrange
            var command = new DeleteBookingCommand
            {
                Id = Guid.NewGuid()
            };

            _mockBookingRepository
                .Setup(repo => repo.DeleteAsync(command.Id))
                .ThrowsAsync(new InvalidOperationException("Booking not found."));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.Handle(command, CancellationToken.None)
            );

            _mockBookingRepository.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
        }
    }
}
