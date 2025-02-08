using Application.Commands.CityCommands;
using Application.Handlers.CityHandlers;
using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Moq;


namespace TABP.Tests.CityTests.CityCommands
{
    public class DeleteCityCommandHandlerTests
    {
        private readonly Mock<CityRepositoryInterface> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DeleteCityHandler _handler;

        public DeleteCityCommandHandlerTests()
        {
            _mockCityRepository = new Mock<CityRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new DeleteCityHandler(_mockCityRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_DeletesCity()
        {
            // Arrange
            var command = new DeleteCityCommand
            {
                Id = Guid.NewGuid()
            };

            _mockCityRepository
                .Setup(repo => repo.IsExistAsync(command.Id))
                .ReturnsAsync(true);

            _mockCityRepository
                .Setup(repo => repo.DeleteAsync(command.Id))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockCityRepository.Verify(repo => repo.IsExistAsync(command.Id), Times.Once);
            _mockCityRepository.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_CityNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var command = new DeleteCityCommand
            {
                Id = Guid.NewGuid()
            };

            _mockCityRepository
                .Setup(repo => repo.IsExistAsync(command.Id))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None)
            );

            _mockCityRepository.Verify(repo => repo.IsExistAsync(command.Id), Times.Once);
            _mockCityRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
