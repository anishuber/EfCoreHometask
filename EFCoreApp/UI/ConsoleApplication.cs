using Data.Data;
using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using UI.UI;

namespace UI;

public sealed class ConsoleApplication(
    CarProductionService carProductionService,
    ICarService carService,
    IManufacturerService manufacturerService,
    IServiceService serviceService,
    CarProductionDbContext context) : IDisposable
{
    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();

            ConsoleMenu.DisplayMenu();

            var option = Console.ReadLine();

            if (string.Equals(option, "0"))
            {
                break;
            }

            switch (option)
            {
                case "1":
                    Console.WriteLine("All cars:\n");
                    ShowCars(await carService.GetAllAsync());
                    Console.WriteLine("All manufacturers:\n");
                    ShowManufacturers(await manufacturerService.GetAllAsync());
                    Console.WriteLine("All services:\n");
                    ShowServices(await serviceService.GetAllAsync());
                    break;
                case "11":
                    ShowCars(await carService.GetAllAsync());
                    break;
                case "12":
                    ShowManufacturers(await manufacturerService.GetAllAsync());
                    break;
                case "13":
                    ShowServices(await serviceService.GetAllAsync());
                    break;
                case "14":
                    ShowCars(await carService.GetByManufacturerAsync(ConsoleMenu.RequestId()));
                    break;
                case "21":
                    await carService.AddAsync(ConsoleMenu.RequestCarData());
                    break;
                case "22":
                    await manufacturerService.AddAsync(ConsoleMenu.RequestManufacturerData());
                    break;
                case "23":
                    await serviceService.AddAsync(ConsoleMenu.RequestServiceData());
                    break;
                case "24":
                    CarDto car = await carProductionService.CreateCarWithManufacturerAsync(ConsoleMenu.RequestCarData(false), ConsoleMenu.RequestManufacturerData());
                    ShowCars(new List<CarDto> { car });
                    break;
                case "31":
                    await carService.DeleteAsync(ConsoleMenu.RequestId());
                    break;
                case "32":
                    await manufacturerService.DeleteAsync(ConsoleMenu.RequestId());
                    break;
                case "33":
                    await serviceService.DeleteAsync(ConsoleMenu.RequestId());
                    break;
                case "41":
                    var carId = ConsoleMenu.RequestId();
                    var carDto = ConsoleMenu.RequestCarData();

                    carDto.VanId = carId;

                    await carService.UpdateAsync(carDto);
                    break;
                case "42":
                    var manufacturerId = ConsoleMenu.RequestId();
                    var manufacturerDto = ConsoleMenu.RequestManufacturerData();

                    manufacturerDto.Id = manufacturerId;

                    await manufacturerService.UpdateAsync(manufacturerDto);
                    break;
                case "43":
                    var serviceId = ConsoleMenu.RequestId();
                    var serviceDto = ConsoleMenu.RequestServiceData();

                    serviceDto.Id = serviceId;

                    await serviceService.UpdateAsync(serviceDto);
                    break;
                default:
                    Console.WriteLine("Enter a valid option.");
                    break;
            }
        }

        Console.WriteLine();
        
    }

    private static void ShowCars(IEnumerable<CarDto> cars)
    {
        foreach (var car in cars)
        {
            Console.WriteLine($"{car.VanId} (by manufacturer#{car.ManufacturerId}): {car.Model} | {car.PlateNumber} | {car.CarType}");
        }
    }

    private static void ShowManufacturers(IEnumerable<ManufacturerDto> manufacturers)
    {
        foreach (var manufacturer in manufacturers)
        {
            Console.WriteLine($"{manufacturer.Id}: {manufacturer.Name} | {manufacturer.Address}");
        }
    }

    private static void ShowServices(IEnumerable<ServiceDto> services)
    {
        foreach (var service in services)
        {
            Console.WriteLine($"{service.Id}: {service.Name} | {service.Address}");
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
