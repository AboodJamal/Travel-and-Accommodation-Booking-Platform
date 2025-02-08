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
    public class GetTrendingCitiesQueryHandlerTests
    {
        private readonly Mock<CityRepositoryInterface> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetTrendingCitiesQueryHandler _handler;

        public GetTrendingCitiesQueryHandlerTests()
        {
            _mockCityRepository = new Mock<CityRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetTrendingCitiesQueryHandler(_mockCityRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsTrendingCities()
        {
            // Arrange
            var query = new GetTrendingCitiesQuery { Count = 5 };

            var trendingCities = new List<City>
        {
            new City { Id = Guid.NewGuid(), Name = "New York" },
            new City { Id = Guid.NewGuid(), Name = "Los Angeles" }
        };

            var trendingCitiesDto = trendingCities.Select(c => new CityNoHotelsDto { Id = c.Id, Name = c.Name }).ToList();

            _mockCityRepository
                .Setup(repo => repo.GetTrendingCitiesAsync(query.Count))
                .ReturnsAsync(trendingCities);

            _mockMapper
                .Setup(mapper => mapper.Map<List<CityNoHotelsDto>>(trendingCities))
                .Returns(trendingCitiesDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockCityRepository.Verify(repo => repo.GetTrendingCitiesAsync(query.Count), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<CityNoHotelsDto>>(trendingCities), Times.Once);
        }
    }
}
