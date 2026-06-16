using Data.Entities;
using Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public static class DataInitializer
{
    public static async Task InitializeAsync(CarProductionDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        if (!await dbContext.Manufacturers.AnyAsync())
        {
            dbContext.Manufacturers.AddRange(CreateManufacturers());
        }

        if (!await dbContext.Services.AnyAsync())
        {
            dbContext.Services.AddRange(CreateServices());
        }

        if (!await dbContext.Cars.AnyAsync())
        {
            dbContext.Cars.AddRange(CreateCars());
        }

        await dbContext.SaveChangesAsync();
    }

    public static Car[] CreateCars()
    {
        return new Car[] {
        new Car { VanId = 1, ManufacturerId = 1, Model = "Toyota Camry", PlateNumber = "AA1001BB", CarType = CarType.Sedan },
        new Car { VanId = 2, ManufacturerId = 2, Model = "Honda Civic", PlateNumber = "AA1002BB", CarType = CarType.Sedan },
        new Car { VanId = 3, ManufacturerId = 1, Model = "Ford Focus", PlateNumber = "AA1003BB", CarType = CarType.Hatchback },
        new Car { VanId = 4, ManufacturerId = 1, Model = "Volkswagen Golf", PlateNumber = "AA1004BB", CarType = CarType.Hatchback },
        new Car { VanId = 5, ManufacturerId = 5, Model = "BMW 4 Series", PlateNumber = "AA1005BB", CarType = CarType.Coupe },
        new Car { VanId = 6, ManufacturerId = 4, Model = "Audi A5", PlateNumber = "AA1006BB", CarType = CarType.Coupe },
        new Car { VanId = 7, ManufacturerId = 4, Model = "Mazda MX-5", PlateNumber = "AA1007BB", CarType = CarType.Convertible },
        new Car { VanId = 8, ManufacturerId = 8, Model = "Ford Mustang", PlateNumber = "AA1008BB", CarType = CarType.Convertible },
        new Car { VanId = 9, ManufacturerId = 9, Model = "Subaru Outback", PlateNumber = "AA1009BB", CarType = CarType.Wagon },
        new Car { VanId = 10, ManufacturerId = 9, Model = "Volvo V60", PlateNumber = "AA1010BB", CarType = CarType.Wagon },
        new Car { VanId = 11, ManufacturerId = 11, Model = "Toyota RAV4", PlateNumber = "AA1011BB", CarType = CarType.Suv },
        new Car { VanId = 12, ManufacturerId = 11, Model = "Honda CR-V", PlateNumber = "AA1012BB", CarType = CarType.Suv },
        new Car { VanId = 13, ManufacturerId = 11, Model = "Nissan Qashqai", PlateNumber = "AA1013BB", CarType = CarType.Crossover },
        new Car { VanId = 14, ManufacturerId = 11, Model = "Hyundai Tucson", PlateNumber = "AA1014BB", CarType = CarType.Crossover },
        new Car { VanId = 15, ManufacturerId = 15, Model = "Ford F-150", PlateNumber = "AA1015BB", CarType = CarType.Pickup },
        new Car { VanId = 16, ManufacturerId = 16, Model = "Toyota Hilux", PlateNumber = "AA1016BB", CarType = CarType.Pickup },
        new Car { VanId = 17, ManufacturerId = 17, Model = "Mercedes-Benz Vito", PlateNumber = "AA1017BB", CarType = CarType.Van },
        new Car { VanId = 18, ManufacturerId = 18, Model = "Volkswagen Transporter", PlateNumber = "AA1018BB", CarType = CarType.Van },
        new Car { VanId = 19, ManufacturerId = 19, Model = "Chrysler Pacifica", PlateNumber = "AA1019BB", CarType = CarType.Minivan },
        new Car { VanId = 20, ManufacturerId = 20, Model = "Renault Espace", PlateNumber = "AA1020BB", CarType = CarType.Minivan },
        new Car { VanId = 21, ManufacturerId = 21, Model = "Toyota RAV4", PlateNumber = "AA1021BB", CarType = CarType.Suv },
        new Car { VanId = 22, ManufacturerId = 22, Model = "Honda CR-V", PlateNumber = "AA1022BB", CarType = CarType.Suv },
        new Car { VanId = 23, ManufacturerId = 23, Model = "Nissan Qashqai", PlateNumber = "AA1023BB", CarType = CarType.Crossover },
        new Car { VanId = 24, ManufacturerId = 23, Model = "Hyundai Tucson", PlateNumber = "AA1024BB", CarType = CarType.Suv },
        new Car { VanId = 25, ManufacturerId = 23, Model = "Ford F-150", PlateNumber = "AA1025BB", CarType = CarType.Pickup },
        new Car { VanId = 26, ManufacturerId = 26, Model = "Toyota Hilux", PlateNumber = "AA1026BB", CarType = CarType.Pickup },
        new Car { VanId = 27, ManufacturerId = 27, Model = "Mercedes-Benz Vito", PlateNumber = "AA1027BB", CarType = CarType.Van },
        new Car { VanId = 28, ManufacturerId = 28, Model = "Volkswagen Transporter", PlateNumber = "AA1028BB", CarType = CarType.Van },
        new Car { VanId = 29, ManufacturerId = 29, Model = "Chrysler Pacifica", PlateNumber = "AA1029BB", CarType = CarType.Minivan },
        new Car { VanId = 30, ManufacturerId = 30, Model = "Renault Espace", PlateNumber = "AA1030BB", CarType = CarType.Minivan },
        };
    }

    public static Service[] CreateServices()
    {
        return new Service[] 
        {
            new Service { Id = 1, Name = "CarFix", Address = "Main Street 1", IsServiceWorking = true },
            new Service { Id = 2, Name = "CarBox", Address = "Second Street 2", IsServiceWorking = true },
            new Service { Id = 3, Name = "WheeFix", Address = "Third Street 3", IsServiceWorking = false },
            new Service { Id = 4, Name = "WheelBox", Address = "Fourth Street 4", IsServiceWorking = true },
            new Service { Id = 5, Name = "FastRepair", Address = "Fifth Street 5", IsServiceWorking = true },
            new Service { Id = 6, Name = "AutoFix", Address = "Sixth Street 6", IsServiceWorking = false },
            new Service { Id = 7, Name = "AutoBox", Address = "Seventh Street 7", IsServiceWorking = true },
            new Service { Id = 8, Name = "MotoFix", Address = "Eighth Street 8", IsServiceWorking = true },
            new Service { Id = 9, Name = "MotoBox", Address = "Ninth Street 9", IsServiceWorking = true },
            new Service { Id = 10, Name = "EasyRepair", Address = "Tenth Street 10", IsServiceWorking = false },
            new Service { Id = 11, Name = "GearFix", Address = "Eleventh Street 11", IsServiceWorking = true },
            new Service { Id = 12, Name = "GearBox", Address = "Twelfth Street 12", IsServiceWorking = true },
            new Service { Id = 13, Name = "RoadFix", Address = "Thirteenth Street 13", IsServiceWorking = false },
            new Service { Id = 14, Name = "RoadBox", Address = "Fourteenth Street 14", IsServiceWorking = true },
            new Service { Id = 15, Name = "SwiftRepair", Address = "Fifteenth Street 15", IsServiceWorking = true },
            new Service { Id = 16, Name = "GarageFix", Address = "Sixteenth Street 16", IsServiceWorking = true },
            new Service { Id = 17, Name = "GarageBox", Address = "Seventeenth Street 17", IsServiceWorking = false },
            new Service { Id = 18, Name = "FixService", Address = "Eighteenth Street 18", IsServiceWorking = true },
            new Service { Id = 19, Name = "BoxService", Address = "Nineteenth Street 19", IsServiceWorking = true },
            new Service { Id = 20, Name = "SafeRepair", Address = "Twentieth Street 20", IsServiceWorking = true },
            new Service { Id = 21, Name = "QuickFix", Address = "Twenty-First Street 21", IsServiceWorking = true },
            new Service { Id = 22, Name = "SpeedyBox", Address = "Twenty-Second Street 22", IsServiceWorking = false },
            new Service { Id = 23, Name = "PrimeRepair", Address = "Twenty-Third Street 23", IsServiceWorking = true },
            new Service { Id = 24, Name = "CityGarage", Address = "Twenty-Fourth Street 24", IsServiceWorking = true },
            new Service { Id = 25, Name = "ProFix", Address = "Twenty-Fifth Street 25", IsServiceWorking = false },
            new Service { Id = 26, Name = "UrbanAuto", Address = "Twenty-Sixth Street 26", IsServiceWorking = true },
            new Service { Id = 27, Name = "RepairHub", Address = "Twenty-Seventh Street 27", IsServiceWorking = true },
            new Service { Id = 28, Name = "ServicePoint", Address = "Twenty-Eighth Street 28", IsServiceWorking = false },
            new Service { Id = 29, Name = "AutoCare", Address = "Twenty-Ninth Street 29", IsServiceWorking = true },
            new Service { Id = 30, Name = "MasterFix", Address = "Thirtieth Street 30", IsServiceWorking = true },
        };
    }
    public static Manufacturer[] CreateManufacturers()
    {
        return new Manufacturer[] 
        {
            new Manufacturer { Id = 1, Name = "AutoWorks", Address = "Main Street 1", IsAChildCompany = false },
            new Manufacturer { Id = 2, Name = "MotorCraft", Address = "Second Street 2", IsAChildCompany = true },
            new Manufacturer { Id = 3, Name = "WheelForge", Address = "Third Street 3", IsAChildCompany = false },
            new Manufacturer { Id = 4, Name = "GearMakers", Address = "Fourth Street 4", IsAChildCompany = true },
            new Manufacturer { Id = 5, Name = "DriveTech", Address = "Fifth Street 5", IsAChildCompany = false },
            new Manufacturer { Id = 6, Name = "AutoBuild", Address = "Sixth Street 6", IsAChildCompany = false },
            new Manufacturer { Id = 7, Name = "MotoParts", Address = "Seventh Street 7", IsAChildCompany = true },
            new Manufacturer { Id = 8, Name = "RoadWorks", Address = "Eighth Street 8", IsAChildCompany = false },
            new Manufacturer { Id = 9, Name = "EngineBox", Address = "Ninth Street 9", IsAChildCompany = true },
            new Manufacturer { Id = 10, Name = "FastMotive", Address = "Tenth Street 10", IsAChildCompany = false },
            new Manufacturer { Id = 11, Name = "PrimeAuto", Address = "Eleventh Street 11", IsAChildCompany = true },
            new Manufacturer { Id = 12, Name = "UrbanMotors", Address = "Twelfth Street 12", IsAChildCompany = false },
            new Manufacturer { Id = 13, Name = "MetalDrive", Address = "Thirteenth Street 13", IsAChildCompany = false },
            new Manufacturer { Id = 14, Name = "CoreParts", Address = "Fourteenth Street 14", IsAChildCompany = true },
            new Manufacturer { Id = 15, Name = "SwiftManufacturing", Address = "Fifteenth Street 15", IsAChildCompany = false },
            new Manufacturer { Id = 16, Name = "GarageWorks", Address = "Sixteenth Street 16", IsAChildCompany = true },
            new Manufacturer { Id = 17, Name = "AutoLine", Address = "Seventeenth Street 17", IsAChildCompany = false },
            new Manufacturer { Id = 18, Name = "MachineHub", Address = "Eighteenth Street 18", IsAChildCompany = false },
            new Manufacturer { Id = 19, Name = "BoxMotors", Address = "Nineteenth Street 19", IsAChildCompany = true },
            new Manufacturer { Id = 20, Name = "SafeParts", Address = "Twentieth Street 20", IsAChildCompany = false },
            new Manufacturer { Id = 21, Name = "QuickBuild", Address = "Twenty-First Street 21", IsAChildCompany = true },
            new Manufacturer { Id = 22, Name = "SpeedyManufacturing", Address = "Twenty-Second Street 22", IsAChildCompany = false },
            new Manufacturer { Id = 23, Name = "PrimeForge", Address = "Twenty-Third Street 23", IsAChildCompany = true },
            new Manufacturer { Id = 24, Name = "CityMotors", Address = "Twenty-Fourth Street 24", IsAChildCompany = false },
            new Manufacturer { Id = 25, Name = "ProParts", Address = "Twenty-Fifth Street 25", IsAChildCompany = true },
            new Manufacturer { Id = 26, Name = "UrbanAutoFactory", Address = "Twenty-Sixth Street 26", IsAChildCompany = false },
            new Manufacturer { Id = 27, Name = "DriveHub", Address = "Twenty-Seventh Street 27", IsAChildCompany = true },
            new Manufacturer { Id = 28, Name = "FactoryPoint", Address = "Twenty-Eighth Street 28", IsAChildCompany = false },
            new Manufacturer { Id = 29, Name = "AutoCareManufacturing", Address = "Twenty-Ninth Street 29", IsAChildCompany = true },
            new Manufacturer { Id = 30, Name = "MasterMotors", Address = "Thirtieth Street 30", IsAChildCompany = false },
        };
    }
}
