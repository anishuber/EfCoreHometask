using Data.Entities;
using Data.Entities.Enums;
using Infrastructure.DTOs;

namespace Infrastructure;

public static class EntityMapper
{
    public static Car MapDtoToCar(CarDto carDto)
    {
        int intType = -1;

        if (!Enum.IsDefined(typeof(CarType), carDto.CarType) &&
            int.TryParse(carDto.CarType, out intType) && !Enum.IsDefined(typeof(CarType), intType))
        {
            throw new ArgumentException($"Car type [{carDto.CarType}] does not exist.");
        }

        return new Car
        {
            VanId = carDto.VanId,
            ManufacturerId = carDto.ManufacturerId,
            Model = carDto.Model,
            PlateNumber = carDto.PlateNumber,
            CarType = intType >= 0 ? (CarType)intType : (CarType)Enum.Parse(typeof(CarType), carDto.CarType)
        };
    }

    public static CarDto MapCarToDto(Car car)
    {
        return new CarDto
        {
            VanId = car.VanId,
            ManufacturerId = car.ManufacturerId,
            Model = car.Model,
            PlateNumber = car.PlateNumber,
            CarType = Enum.GetName(car.CarType) ?? string.Empty
        };
    }

    public static Manufacturer MapDtoToManufacturer(ManufacturerDto manufacturerDto)
    {
        return new Manufacturer
        {
            Id = manufacturerDto.Id,
            Name = manufacturerDto.Name,
            Address = manufacturerDto.Address,
            IsAChildCompany = manufacturerDto.IsAChildCompany,
        };
    }

    public static ManufacturerDto MapManufacturerToDto(Manufacturer manufacturer)
    {
        return new ManufacturerDto
        {
            Id = manufacturer.Id,
            Name = manufacturer.Name,
            Address = manufacturer.Address,
            IsAChildCompany = manufacturer.IsAChildCompany,
        };
    }

    public static Data.Entities.Service MapDtoToService(DTOs.ServiceDto serviceDto)
    {
        return new Data.Entities.Service
        {
            Id = serviceDto.Id,
            Name = serviceDto.Name,
            Address = serviceDto.Address,
            IsServiceWorking = serviceDto.IsServiceWorking,
        };
    }

    public static DTOs.ServiceDto MapServiceToDto(Data.Entities.Service service)
    {
        return new DTOs.ServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Address = service.Address,
            IsServiceWorking = service.IsServiceWorking,
        };
    }
}