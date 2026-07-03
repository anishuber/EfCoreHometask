using Data.Data;
using Data.Interfaces;
using Data.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCarProductionServices(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<CarProductionDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<CarProductionService>();

        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IManufacturerService, ManufacturerService>();
        services.AddScoped<IServiceService, ServiceService>();

        services.AddScoped<ConsoleApplication>();

        return services;
    }
}