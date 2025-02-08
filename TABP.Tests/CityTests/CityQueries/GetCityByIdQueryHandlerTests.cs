using Application.Handlers.CityHandlers;
using Application.Queries.CityQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TABP.Application.DTOs.CityDtos;
using TABP.Domain.Entities;

namespace TABP.Tests.CityTests.CityQueries
{
    public class GetCityByIdQueryHandlerTests
    {
        private readonly Mock<CityRepositoryInterface> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetCityByIdQueryHandler _handler;

        public GetCityByIdQueryHandlerTests()
        {
            _mockCityRepository = new Mock<CityRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetCityByIdQueryHandler(_mockCityRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsCityDto()
        {
            // Arrange
            var query = new GetCityByIdQuery
            {
                Id = Guid.NewGuid(),
                IncludeHotels = true
            };

            var city = new City { Id = query.Id, Name = "New York" };
            var cityDto = new CityDto { Id = city.Id, Name = city.Name };

            _mockCityRepository
                .Setup(repo => repo.GetByIdAsync(query.Id, query.IncludeHotels))
                .ReturnsAsync(city);

            _mockMapper
                .Setup(mapper => mapper.Map<CityDto>(city))
                .Returns(cityDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityDto.Id, result.Id);
            Assert.Equal(cityDto.Name, result.Name);
            _mockCityRepository.Verify(repo => repo.GetByIdAsync(query.Id, query.IncludeHotels), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<CityDto>(city), Times.Once);
        }

        [Fact]
        public async Task Handle_CityNotFound_ReturnsNull()
        {
            // Arrange
            var query = new GetCityByIdQuery
            {
                Id = Guid.NewGuid(),
                IncludeHotels = true
            };

            _mockCityRepository
                .Setup(repo => repo.GetByIdAsync(query.Id, query.IncludeHotels))
                .ReturnsAsync((City?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _mockCityRepository.Verify(repo => repo.GetByIdAsync(query.Id, query.IncludeHotels), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<CityDto>(It.IsAny<City>()), Times.Never);
        }
    }
}
