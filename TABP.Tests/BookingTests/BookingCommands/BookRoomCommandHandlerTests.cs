using Application.Commands.BookingCommands;
using Application.Handlers.BookingHandlers;
using Infrastructure.Interfaces;
using AutoMapper;
using Moq;
using Xunit;
using TABP.Application.DTOs.BookingDtos;
using TABP.Domain.Entities;

namespace TABP.Tests.BookingTests.BookingCommands
{
    public class BookRoomCommandHandlerTests
    {
        private readonly Mock<BookingRepositoryInterface> _mockBookingRepository;
        private readonly Mock<UserRepositoryInterface> _mockUserRepository;
        private readonly Mock<RoomRepositoryInterface> _mockRoomRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookRoomCommandHandler _handler;

        public BookRoomCommandHandlerTests()
        {
            _mockBookingRepository = new Mock<BookingRepositoryInterface>();
            _mockUserRepository = new Mock<UserRepositoryInterface>();
            _mockRoomRepository = new Mock<RoomRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new BookRoomCommandHandler(
                _mockBookingRepository.Object,
                _mockMapper.Object,
                _mockUserRepository.Object,
                _mockRoomRepository.Object
            );
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsBookingDto()
        {
            // Arrange
            var command = new BookRoomCommand
            {
                RoomId = Guid.NewGuid(),
                GuestEmail = "guest@example.com",
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(3)
            };

            var guestId = Guid.NewGuid();
            var roomPrice = 150.0f;
            var booking = new Booking { Id = Guid.NewGuid() };
            var bookingDto = new BookingDto { Id = booking.Id };

            _mockUserRepository
                .Setup(repo => repo.GetGuestIdByEmailAsync(command.GuestEmail))
                .ReturnsAsync(guestId);

            _mockRoomRepository
                .Setup(repo => repo.GetPriceForRoomWithDiscount(command.RoomId))
                .ReturnsAsync(roomPrice);

            _mockMapper
                .Setup(mapper => mapper.Map<Booking>(command))
                .Returns(booking);

            _mockBookingRepository
                .Setup(repo => repo.InsertAsync(booking))
                .ReturnsAsync(booking);

            _mockMapper
                .Setup(mapper => mapper.Map<BookingDto>(booking))
                .Returns(bookingDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookingDto.Id, result.Id);
            _mockUserRepository.Verify(repo => repo.GetGuestIdByEmailAsync(command.GuestEmail), Times.Once);
            _mockRoomRepository.Verify(repo => repo.GetPriceForRoomWithDiscount(command.RoomId), Times.Once);
            _mockBookingRepository.Verify(repo => repo.InsertAsync(booking), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidGuestEmail_ThrowsException()
        {
            // Arrange
            var command = new BookRoomCommand
            {
                RoomId = Guid.NewGuid(),
                GuestEmail = "sldkfjsldkfjkldj@gmail.com",
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(3)
            };

            _mockUserRepository
                .Setup(repo => repo.GetGuestIdByEmailAsync(command.GuestEmail))
                .ReturnsAsync((Guid?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.Handle(command, CancellationToken.None)
            );

            _mockUserRepository.Verify(repo => repo.GetGuestIdByEmailAsync(command.GuestEmail), Times.Once);
            _mockRoomRepository.Verify(repo => repo.GetPriceForRoomWithDiscount(It.IsAny<Guid>()), Times.Never);
            _mockBookingRepository.Verify(repo => repo.InsertAsync(It.IsAny<Booking>()), Times.Never);
        }
    }
}
