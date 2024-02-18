using FluentValidation;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using SystemLotowMK.Application.Dto;
using SystemLotowMK.Application.Interfaces;
using SystemLotowMK.Application.Jobs;
using SystemLotowMK.Application.Services;
using SystemLotowMK.Domain.Entities;

namespace SystemLotowMK.Application;

public static class ModuleInitializer
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IJob, PaymentObserver>();
        
        services.AddValidations();
        return services;
    }
    
    private static void AddValidations(this IServiceCollection services)
    {
        services.AddSingleton<IValidator<FlightDto>, FlightDtoValidator>();
        services.AddSingleton<IValidator<ReservationDto>, ReservationDtoValidator>();
    }
    
    // public static IServiceProvider ConfigureApplicationModule(this IServiceProvider serviceProvider)
    // {
    //     var paymentObserver = serviceProvider.GetRequiredService<PaymentObserver>();
    //     
    //     RecurringJob.AddOrUpdate(() => paymentObserver.Observe(), Cron.Minutely);
    //     
    //     return serviceProvider;
    // }
}