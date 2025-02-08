using Application.Handlers.BookingHandlers;
using Application.Queries.BookingQueries;
using AutoMapper;
using Infrastructure.ExtraModels;
using Infrastructure.Interfaces;
using Moq;
using TABP.Application.DTOs.BookingDtos;

namespace TABP.Tests.BookingTests.BookingQueries
{
    public class GetInvoiceByBookingIdQueryHandlerTests
    {
        private readonly Mock<BookingRepositoryInterface> _mockBookingRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetInvoiceByBookingIdQueryHandler _handler;

        public GetInvoiceByBookingIdQueryHandlerTests()
        {
            _mockBookingRepository = new Mock<BookingRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetInvoiceByBookingIdQueryHandler(_mockBookingRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsInvoiceDto()
        {
            // Arrange
            var query = new GetInvoiceByBookingIdQuery
            {
                BookingId = Guid.NewGuid()
            };

            var invoice = new Invoice { Id = Guid.NewGuid() };
            var invoiceDto = new InvoiceDto { Id = invoice.Id };

            _mockBookingRepository
                .Setup(repo => repo.GetInvoiceByBookingIdAsync(query.BookingId))
                .ReturnsAsync(invoice);

            _mockMapper
                .Setup(mapper => mapper.Map<InvoiceDto>(invoice))
                .Returns(invoiceDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invoiceDto.Id, result.Id);
            _mockBookingRepository.Verify(repo => repo.GetInvoiceByBookingIdAsync(query.BookingId), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<InvoiceDto>(invoice), Times.Once);
        }
    }
}
