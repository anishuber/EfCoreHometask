using Infrastructure.DTOs;

namespace UI.UI;

internal static class ConsoleMenu
{
    public static void DisplayMenu()
    {
        Console.WriteLine("[1] Display all data");

        Console.WriteLine("[11] Display Cars");
        Console.WriteLine("[12] Display Manufacturers");
        Console.WriteLine("[13] Display Services");

        Console.WriteLine("[14] Display Cars by Manufacturer");

        Console.WriteLine("[21] Add Car");
        Console.WriteLine("[22] Add Manufacturer");
        Console.WriteLine("[23] Add Service");

        Console.WriteLine("[24] Add Car to a new Manufacturer");

        Console.WriteLine("[31] Delete Car");
        Console.WriteLine("[32] Delete Manufacturer");
        Console.WriteLine("[33] Delete Service");

        Console.WriteLine("[41] Update Car");
        Console.WriteLine("[42] Update Manufacturer");
        Console.WriteLine("[43] Update Service");

        Console.WriteLine("[0] Exit");
    }

    public static CarDto RequestCarData(bool hasManufacturer = true)
    {
        Console.WriteLine("Enter the car data");

        var carModel = ProcessInput("car model", string.Empty);
        int carManufacturer = 0;
        
        if (hasManufacturer)
        {
            carManufacturer = RequestId("car manufacturer");
        }

        var plateNumber = ProcessInput("plate number", string.Empty);
        var carType = ProcessInput("car type", "(number or name)");

        return new CarDto
        {
            Model = carModel,
            ManufacturerId = carManufacturer,
            PlateNumber = plateNumber,
            CarType = carType,
        };
    }

    public static ManufacturerDto RequestManufacturerData()
    {
        Console.WriteLine("Enter the manufacturer data");
        var manufacturerName = ProcessInput("manufacturer name", string.Empty);
        var manufacturerAddress = ProcessInput("manufacturer address", string.Empty);
        var manufacturerStatus = ProcessBooleanInput("if a manufacturer is a child company", "([1] yes / [0] no)");

        return new ManufacturerDto
        {
            Name = manufacturerName,
            Address = manufacturerAddress,
            IsAChildCompany = manufacturerStatus,
        };
    }

    public static ServiceDto RequestServiceData()
    {
        Console.WriteLine("Enter the service data");
        var serviceName = ProcessInput("service name", string.Empty);
        var serviceAddress = ProcessInput("service address", string.Empty);
        var serviceStatus = ProcessBooleanInput("if a service is working", "([1] yes / [0] no)");

        return new ServiceDto
        {
            Name = serviceName,
            Address = serviceAddress,
            IsServiceWorking = serviceStatus,
        };
    }

    public static int RequestId(string paramName = "entity")
    {
        Console.WriteLine($"Enter the ID of the {paramName}.");
        int id;


        while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
        {
            Console.WriteLine("Invalid ID provided");
        }

        return id;
    }

    private static string ProcessInput(string paramName, string instructions)
    {
        Console.WriteLine($"Enter the {paramName} {instructions}: ");
        var input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine($"{paramName} cannot be empty");
            input = Console.ReadLine();
        }

        return input.Trim();
    }

    private static bool ProcessBooleanInput(string paramName, string instructions)
    {
        while (true)
        {
            Console.WriteLine($"Enter the {paramName} {instructions}: ");
            string? input = Console.ReadLine()?.Trim();

            if (string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase) ||
                input == "1")
            {
                return true;
            }

            if (string.Equals(input, "no", StringComparison.OrdinalIgnoreCase) ||
                input == "0")
            {
                return false;
            }

            Console.WriteLine("Invalid value. Enter \"yes\", \"no\", \"1\", or \"0\".");
        }
    }
}
