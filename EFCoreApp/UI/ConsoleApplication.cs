using Data.CustomExceptions;
using Data.Data;
using Infrastructure.CustomExceptions;
using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using UI.UI;

namespace UI;

public sealed class ConsoleApplication(
    CarProductionService carProductionService,
    ICarService carService,
    IManufacturerService manufacturerService,
    IServiceService serviceService,
    CarProductionDbContext context)
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

            try
            {
                await ExecuteMenuOptionAsync(option);
            }
            catch (ServiceException ex)
            {
                PrintError(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                PrintError(ex.Message);
            }
            catch (ArgumentException ex)
            {
                PrintError(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                PrintError("A database update error occurred. The operation could not be completed.");

                if (ex.InnerException is not null)
                {
                    PrintError($"Details: {ex.InnerException.Message}");
                }
            }
            catch (InvalidOperationException ex)
            {
                PrintError(ex.Message);
            }
            catch (Exception ex)
            {
                PrintError("An unexpected error occurred.");
                PrintError(ex.Message);
            }
        }

        Console.WriteLine();
    }

    private async Task ExecuteMenuOptionAsync(string? option)
    {
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
                Console.WriteLine("Car was added successfully.");
                break;

            case "22":
                await manufacturerService.AddAsync(ConsoleMenu.RequestManufacturerData());
                Console.WriteLine("Manufacturer was added successfully.");
                break;

            case "23":
                await serviceService.AddAsync(ConsoleMenu.RequestServiceData());
                Console.WriteLine("Service was added successfully.");
                break;

            case "24":
                CarDto car = await carProductionService.CreateCarWithManufacturerAsync(
                    ConsoleMenu.RequestCarData(false),
                    ConsoleMenu.RequestManufacturerData());

                Console.WriteLine("Car with manufacturer was created successfully.");
                ShowCars(new List<CarDto> { car });
                break;

            case "31":
                await carService.DeleteAsync(ConsoleMenu.RequestId());
                Console.WriteLine("Car was deleted successfully.");
                break;

            case "32":
                await manufacturerService.DeleteAsync(ConsoleMenu.RequestId());
                Console.WriteLine("Manufacturer was deleted successfully.");
                break;

            case "33":
                await serviceService.DeleteAsync(ConsoleMenu.RequestId());
                Console.WriteLine("Service was deleted successfully.");
                break;

            case "41":
                var carId = ConsoleMenu.RequestId();
                var carDto = ConsoleMenu.RequestCarData();

                carDto.VanId = carId;

                await carService.UpdateAsync(carDto);
                Console.WriteLine("Car was updated successfully.");
                break;

            case "42":
                var manufacturerId = ConsoleMenu.RequestId();
                var manufacturerDto = ConsoleMenu.RequestManufacturerData();

                manufacturerDto.Id = manufacturerId;

                await manufacturerService.UpdateAsync(manufacturerDto);
                Console.WriteLine("Manufacturer was updated successfully.");
                break;

            case "43":
                var serviceId = ConsoleMenu.RequestId();
                var serviceDto = ConsoleMenu.RequestServiceData();

                serviceDto.Id = serviceId;

                await serviceService.UpdateAsync(serviceDto);
                Console.WriteLine("Service was updated successfully.");
                break;

            default:
                Console.WriteLine("Enter a valid option.");
                break;
        }
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

    private static void PrintError(string message)
    {
        Console.WriteLine($"Error: {message}");
    }
}
