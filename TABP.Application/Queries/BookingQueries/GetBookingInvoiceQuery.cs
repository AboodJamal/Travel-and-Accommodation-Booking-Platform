using TABP.Application.DTOs.BookingDtos;
using MediatR;

namespace Application.Queries.BookingQueries;

public record GetInvoiceByBookingIdQuery : IRequest<InvoiceDto>
{
    public Guid BookingId { get; set; }
}