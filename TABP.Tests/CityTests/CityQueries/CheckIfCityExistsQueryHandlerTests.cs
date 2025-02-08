using Application.Handlers.CityHandlers;
using Application.Queries.CityQueries;
using Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TABP.Tests.CityTests.CityQueries
{
    public class CheckIfCityExistsQueryHandlerTests
    {
        private readonly Mock<CityRepositoryInterface> _mockCityRepository;
        private readonly CheckIfCityExistsQueryHandler _handler;

        public CheckIfCityExistsQueryHandlerTests()
        {
            _mockCityRepository = new Mock<CityRepositoryInterface>();
            _handler = new CheckIfCityExistsQueryHandler(_mockCityRepository.Object);
        }

        [Fact]
        public async Task Handle_CityExists_ReturnsTrue()
        {
            // Arrange
            var query = new CheckIfCityExistsQuery { Id = Guid.NewGuid() };

            _mockCityRepository
                .Setup(repo => repo.IsExistAsync(query.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mockCityRepository.Verify(repo => repo.IsExistAsync(query.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_CityDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var query = new CheckIfCityExistsQuery { Id = Guid.NewGuid() };

            _mockCityRepository
                .Setup(repo => repo.IsExistAsync(query.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mockCityRepository.Verify(repo => repo.IsExistAsync(query.Id), Times.Once);
        }
    }
}
