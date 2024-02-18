using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemLotowMK.Domain.Interfaces.Infrastructure;
using SystemLotowMK.Infrastructure.ApplicationContexts;
using SystemLotowMK.Infrastructure.Repositories;

namespace SystemLotowMK.Infrastructure;


public static class ModuleInitializer
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        return services;
    }
}