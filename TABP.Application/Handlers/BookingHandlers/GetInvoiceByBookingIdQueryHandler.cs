using TABP.Application.DTOs.BookingDtos;
using Application.Queries.BookingQueries;
using AutoMapper;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.BookingHandlers;

public class GetInvoiceByBookingIdQueryHandler : IRequestHandler<GetInvoiceByBookingIdQuery, InvoiceDto>
{
    private readonly BookingRepositoryInterface _bookingRepository;
    private readonly IMapper _mapper;

    public GetInvoiceByBookingIdQueryHandler(BookingRepositoryInterface bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(GetInvoiceByBookingIdQuery request,
    CancellationToken cancellationToken)
    {
        return _mapper.Map<InvoiceDto>(await _bookingRepository
        .GetInvoiceByBookingIdAsync(request.BookingId));
    }
}