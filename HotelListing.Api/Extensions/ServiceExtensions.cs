using HotelListing.Api.Data;
using HotelListing.Api.Core.IServices;
using HotelListing.Api.Service;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Filters;
using HotelListing.Api.Repository;
using HotelListing.Api.Services;
using HotelListing.Api.Core.IRepository;
using Microsoft.AspNetCore.SignalR;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelListing.Api.Validators;


namespace HotelListing.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();
            services.AddScoped<IdempotencyFilter>();
            services.AddScoped<TransactionFilter>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateCountryValidator>();

            return services;
        }

        public static IServiceCollection AddDatabaseservices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HotelListingDbContext>(options =>
                options.UseNpgsql(connectionString));
            
            return services;
        } 

        public static IServiceCollection AddControllerServices(
            this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<TransactionFilter>();
                options.Filters.Add<IdempotencyFilter>();
            })
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            );

            return services;
        }
    }
}