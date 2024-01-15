using System.Reflection;
using Core.Repositories;
using Core.Services;
using Core.Upgrades;
using Microsoft.Extensions.DependencyInjection;
using ORM.Contract.Interfaces;

namespace Core;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddSingleton<IBookingService, BookingService>();
        services.AddSingleton<IFlightService, FlightService>();
        services.AddSingleton<IPaymentService, PaymentService>();
        
        services.AddSingleton<ISqlExecutor, Upgrade1>();
        services.AddSingleton<ISqlExecutor, Upgrade2>();
        services.AddSingleton<ISqlExecutor, Upgrade3>();

        services.AddSingleton<IReservationRepository, ReservationRepository>();
        services.AddSingleton<IFlightRepository, FlightRepository>();
    }
}

public class MappingFromAssembly : IMappingFromAssembly
{
    public Assembly Assembly => Assembly.GetExecutingAssembly();
}