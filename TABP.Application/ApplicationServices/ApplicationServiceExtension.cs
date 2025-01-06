using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TABP.Application.ApplicationServices.ProfileMappers;

namespace TABP.Application.ApplicationServices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
        services.AddAutoMapper(typeof(RoomAmenityProfileMapper));
        services.AddAutoMapper(typeof(ReviewProfileMapper));
        services.AddAutoMapper(typeof(RoomProfileMapper));
        services.AddAutoMapper(typeof(DiscountProfileMapper));
        services.AddAutoMapper(typeof(CityProfileMapper));
        services.AddAutoMapper(typeof(HotelProfileMapper));
        return services;
    }
}