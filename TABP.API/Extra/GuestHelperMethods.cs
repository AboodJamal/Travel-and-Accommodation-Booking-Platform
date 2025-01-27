using Application.Queries.BookingQueries;
using Application.Queries.RoomQueries;
using MediatR;
using System.Security.Claims;
using Application.Commands.BookingCommands;
using TABP.Application.DTOs.BookingDtos;
using Application.Queries.UserQueries;
using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.EmailServices;
using TABP.Infrastructure.EmailServices.EmailService;
using Infrastructure.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Extra;
using TABP.API.Features.Booking.BookingValidators;
using TAABP.API.Extra;

namespace TABP.API.Extra
{
    public class GuestHelperMethods
    {
        private readonly IMediator _mediator;
        public GuestHelperMethods(IMediator mediator)
        {
            _mediator = mediator;
        }

       public async Task<bool> CheckIfBookingAccessibleForGuestAsync(Guid bookingId, string? guestEmail)
        {
            return await _mediator.Send(new CheckIfBookingExistsForGuestQuery
            {
                BookingId = bookingId,
                GuestEmail = guestEmail
            });
        }

        public async Task<bool> CheckIfBookingExistsAsync(Guid bookingId)
        {
            return await _mediator.Send(new CheckIfBookingExistsQuery
            {
                Id = bookingId
            });
        }

        public async Task<bool> CheckIfRoomExistsAsync(Guid bookingId)
        {
            return await _mediator.Send(new CheckIfRoomExistsQuery
            {
                Id = bookingId
            });
        }
    }
}
