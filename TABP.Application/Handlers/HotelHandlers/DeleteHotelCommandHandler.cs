using Application.Commands.HotelCommands;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.HotelHandlers;

public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
{
    private readonly HotelRepositoryInterface _hotelRepository;
    private readonly IMapper _mapper;

    public DeleteHotelCommandHandler(HotelRepositoryInterface hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        if (!await _hotelRepository.IsExistAsync(request.Id))
        {
            throw new NotFoundException($"Hotel with ID: {request.Id} ");
        }
        await _hotelRepository.DeleteAsync(request.Id);
    }
}