using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Models;
using FlightPlanner.Services.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace FlightPlanner.Services
{
    public static class DependencyResolutionUtils
    {
        public static void RegisterValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidate, FlightValidator>();
            services.AddScoped<IValidate, FlightCarrierValidator>();
            services.AddScoped<IValidate, FlightTimesValidator>();
            services.AddScoped<IValidate, AirportValidator>();
            services.AddScoped<IValidate, AirportPropsValidator>();
            services.AddScoped<IValidate, FlightTimesIntervalValidator>();
            services.AddScoped<IValidate, FlightAirportValidator>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<ISearchFlightService, SearchFlightService>();
        }
    }
}
