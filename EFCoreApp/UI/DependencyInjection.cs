using Data.Data;
using Data.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace UI;

public class DependencyInjection
{
    private readonly CarProductionDbContext? dbContext = null;

    public CarProductionService GetCarProductionService()
    {
        var context = GetDbContext();

        var carRepository = new CarRepository(context);
        var manufacturerRepository = new ManufacturerRepository(context);
        var serviceRepository = new ServiceRepository(context);

        var unitOfWork = new UnitOfWork(
            context,
            carRepository,
            manufacturerRepository,
            serviceRepository);

        return new CarProductionService(context, unitOfWork);
    }

    public ICarService GetCarService()
    {
        var context = GetDbContext();

        var carRepository = new CarRepository(context);
        var manufacturerRepository = new ManufacturerRepository(context);
        var serviceRepository = new ServiceRepository(context);

        var unitOfWork = new UnitOfWork(
            context,
            carRepository,
            manufacturerRepository,
            serviceRepository);

        return new CarService(unitOfWork);
    }

    public IManufacturerService GetManufacturerService()
    {
        var context = GetDbContext();

        var carRepository = new CarRepository(context);
        var manufacturerRepository = new ManufacturerRepository(context);
        var serviceRepository = new ServiceRepository(context);

        var unitOfWork = new UnitOfWork(
            context,
            carRepository,
            manufacturerRepository,
            serviceRepository);

        return new ManufacturerService(unitOfWork);
    }

    public IServiceService GetServiceService()
    {
        var context = GetDbContext();

        var carRepository = new CarRepository(context);
        var manufacturerRepository = new ManufacturerRepository(context);
        var serviceRepository = new ServiceRepository(context);

        var unitOfWork = new UnitOfWork(
            context,
            carRepository,
            manufacturerRepository,
            serviceRepository);

        return new ServiceService(unitOfWork);
    }

    public ConsoleApplication GetConsoleApplication()
    {
        var carProductionService = GetCarProductionService();

        return new ConsoleApplication(carProductionService, GetCarService(), GetManufacturerService(), GetServiceService(), GetDbContext());
    }

    public async Task InitializeDatabaseAsync()
    {
        await using var context = GetDbContext();

        await DataInitializer.InitializeAsync(context);
    }

    private CarProductionDbContext GetDbContext()
    {
        if (dbContext is null)
        {
            var options = new DbContextOptionsBuilder<CarProductionDbContext>().Options;

            return new CarProductionDbContext(options);
        }

        return dbContext;
    }
}
