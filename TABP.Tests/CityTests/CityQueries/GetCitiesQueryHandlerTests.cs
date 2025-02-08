using Application.Handlers.CityHandlers;
using Application.Queries.CityQueries;
using AutoMapper;
using Infrastructure.ExtraModels;
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
    public class GetCitiesQueryHandlerTests
    {
        private readonly Mock<CityRepositoryInterface> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetCitiesQueryHandler _handler;

        public GetCitiesQueryHandlerTests()
        {
            _mockCityRepository = new Mock<CityRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetCitiesQueryHandler(_mockCityRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsPaginatedList()
        {
            // Arrange
            var query = new GetCitiesQuery
            {
                IncludeHotels = false,
                SearchQuery = "New York",
                PageNumber = 1,
                PageSize = 10
            };

            var cities = new List<City>
        {
            new City { Id = Guid.NewGuid(), Name = "New York" },
            new City { Id = Guid.NewGuid(), Name = "Los Angeles" }
        };

            var paginatedList = new PaginatedList<City>(cities, new PageData(2, 1, 10));

            _mockCityRepository
                .Setup(repo => repo.GetAllAsync(query.IncludeHotels, query.SearchQuery, query.PageNumber, query.PageSize))
                .ReturnsAsync(paginatedList);

            _mockMapper
                .Setup(mapper => mapper.Map<List<CityDto>>(cities))
                .Returns(cities.Select(c => new CityDto { Id = c.Id, Name = c.Name }).ToList());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count);
            _mockCityRepository.Verify(repo => repo.GetAllAsync(query.IncludeHotels, query.SearchQuery, query.PageNumber, query.PageSize), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<CityDto>>(cities), Times.Once);
        }
    }
}
