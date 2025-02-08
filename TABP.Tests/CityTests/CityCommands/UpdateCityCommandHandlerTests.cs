using Application.Commands.CityCommands;
using Application.Handlers.CityHandlers;
using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Moq;
using TABP.Domain.Entities;

namespace TABP.Tests.CityTests.CityCommands
{
    public class UpdateCityCommandHandlerTests
    {
        private readonly Mock<CityRepositoryInterface> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateCityHandler _handler;

        public UpdateCityCommandHandlerTests()
        {
            _mockCityRepository = new Mock<CityRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateCityHandler(_mockCityRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesCity()
        {
            // Arrange
            var command = new UpdateCityCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated City",
                CountryName = "Updated Country",
                CountryCode = "UC",
                PostOffice = "UPD"
            };

            var city = new City { Id = command.Id };

            _mockCityRepository
                .Setup(repo => repo.IsExistAsync(command.Id))
                .ReturnsAsync(true);

            _mockMapper
                .Setup(mapper => mapper.Map<City>(command))
                .Returns(city);

            _mockCityRepository
                .Setup(repo => repo.UpdateAsync(city))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockCityRepository.Verify(repo => repo.IsExistAsync(command.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<City>(command), Times.Once);
            _mockCityRepository.Verify(repo => repo.UpdateAsync(city), Times.Once);
        }

        [Fact]
        public async Task Handle_CityNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var command = new UpdateCityCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated City",
                CountryName = "Updated Country",
                CountryCode = "UC",
                PostOffice = "UPD"
            };

            _mockCityRepository
                .Setup(repo => repo.IsExistAsync(command.Id))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None)
            );

            _mockCityRepository.Verify(repo => repo.IsExistAsync(command.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<City>(It.IsAny<UpdateCityCommand>()), Times.Never);
            _mockCityRepository.Verify(repo => repo.UpdateAsync(It.IsAny<City>()), Times.Never);
        }
    }
}
