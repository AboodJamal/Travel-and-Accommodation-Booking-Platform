using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TABP.Infrastructure.EmailServices.EmailService;
using Infrastructure.Interfaces;
using Infrastructure.EmailServices;
using TABP.Infrastructure.Authentication.Generators;
using TABP.Hashing.PasswordUtils;
using TABP.Infrastructure.Authentication.User;
using Infrastructure.ImageServices;
using Infrastructure.Pdf;


namespace TABP.Infrastructure.InfrastructureServices;
    public static class InfrastructureServiceExtension
{
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<CityRepositoryInterface, CityRepository>();
            services.AddScoped<UserRepositoryInterface, UserRepository>();
            services.AddTransient<TokenGeneratorInterface, TokenGenerator>();
            services.AddTransient<PasswordHandlerInterface, PasswordHandler>();
            services.AddScoped<AuthUserInterface, AuthUser>();
            services.AddScoped<ImageServiceInterface, CloudinaryImageService>();
            services.AddScoped<RoomAmenityRepositoryInterface, RoomAmenityRepository>();
            services.AddScoped<ReviewRepositoryInterface, ReviewRepository>();
            services.AddScoped<BookingRepositoryInterface, BookingRepository>();
            services.AddScoped<RoomRepositoryInterface, RoomRepository>();
            services.AddScoped<HotelRepositoryInterface, HotelRepository>();
            services.AddScoped<RoomTypeRepositoryInterface, RoomTypeRepository>();
            services.AddScoped<PdfServiceInterface, PdfService>();
            services.AddScoped<DiscountRepositoryInterface, DiscountRepository>();
            services.AddScoped<EmailServiceInterface, EmailService>();
            return services;
        }
    }

