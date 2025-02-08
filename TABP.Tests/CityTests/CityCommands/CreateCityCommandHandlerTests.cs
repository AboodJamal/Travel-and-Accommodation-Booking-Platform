using Application.Commands.CityCommands;
using Application.Handlers.CityHandlers;
using AutoMapper;
using Infrastructure.Interfaces;
using Moq;
using TABP.Application.DTOs.CityDtos;
using TABP.Domain.Entities;

namespace TABP.Tests.CityTests.CityCommands
{
    public class CreateCityCommandHandlerTests
    {
        private readonly Mock<CityRepositoryInterface> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateCityCommandHandler _handler;

        public CreateCityCommandHandlerTests()
        {
            _mockCityRepository = new Mock<CityRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateCityCommandHandler(_mockCityRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsCityNoHotelsDto()
        {
            // Arrange
            var command = new CreateCityCommand
            {
                Name = "New York",
                CountryName = "United States",
                CountryCode = "US",
                PostOffice = "NYC"
            };

            var city = new City { Id = Guid.NewGuid() };
            var cityDto = new CityNoHotelsDto { Id = city.Id };

            _mockMapper
                .Setup(mapper => mapper.Map<City>(command))
                .Returns(city);

            _mockCityRepository
                .Setup(repo => repo.InsertAsync(city))
                .ReturnsAsync(city);

            _mockMapper
                .Setup(mapper => mapper.Map<CityNoHotelsDto>(city))
                .Returns(cityDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityDto.Id, result.Id);
            _mockMapper.Verify(mapper => mapper.Map<City>(command), Times.Once);
            _mockCityRepository.Verify(repo => repo.InsertAsync(city), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<CityNoHotelsDto>(city), Times.Once);
        }

        [Fact]
        public async Task Handle_CityCreationFails_ReturnsNull()
        {
            // Arrange
            var command = new CreateCityCommand
            {
                Name = "New York",
                CountryName = "United States",
                CountryCode = "US",
                PostOffice = "NYC"
            };

            var city = new City { Id = Guid.NewGuid() };

            _mockMapper
                .Setup(mapper => mapper.Map<City>(command))
                .Returns(city);

            _mockCityRepository
                .Setup(repo => repo.InsertAsync(city))
                .ReturnsAsync((City?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _mockMapper.Verify(mapper => mapper.Map<City>(command), Times.Once);
            _mockCityRepository.Verify(repo => repo.InsertAsync(city), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<CityNoHotelsDto>(It.IsAny<City>()), Times.Never);
        }
    }

}
