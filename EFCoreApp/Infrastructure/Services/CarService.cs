using Data.Entities;
using Data.Entities.Enums;
using Data.Interfaces;
using Infrastructure.CustomExceptions;
using Infrastructure.DTOs;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class CarService : ICarService
{
    private readonly IUnitOfWork unitOfWork;

    public CarService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<CarDto> AddAsync(CarDto dto)
    {
        Validate(dto);

        var existingManufacturer = await this.unitOfWork.ManufacturerRepository.GetByIdAsync(dto.ManufacturerId);

        if (existingManufacturer is null)
        {
            throw new ArgumentException($"Manufacturer with id {dto.ManufacturerId} does not exist.");
        }

        var existingCar = await this.unitOfWork.CarRepository.GetByIdAsync(dto.VanId);

        if (existingCar is not null)
        {
            throw new ArgumentException($"Car with id {dto.VanId} already exists.");
        }

        var car = EntityMapper.MapDtoToCar(dto);

        await this.unitOfWork.CarRepository.AddAsync(car);

        await unitOfWork.SaveAsync();

        return EntityMapper.MapCarToDto(car);
    }

    public async Task DeleteAsync(int id)
    {
        await this.unitOfWork.CarRepository.DeleteByIdAsync(id);
        await this.unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        IEnumerable<Car> cars = await this.unitOfWork.CarRepository.GetAllAsync();

        return cars
            .Select(EntityMapper.MapCarToDto)
            .ToList();
    }

    public async Task<CarDto> GetByIdAsync(int id)
    {
        Car? car = await this.unitOfWork.CarRepository.GetByIdAsync(id);

        return car is null ?
            throw new ServiceException($"Car with ID {id} was not found") :
            EntityMapper.MapCarToDto(car);
    }

    public async Task<List<CarDto>> GetByManufacturerAsync(int manufacturerId)
    {
        IEnumerable<Car> cars = await this.unitOfWork.CarRepository.GetByManufacturer(manufacturerId);

        return cars
            .Select(EntityMapper.MapCarToDto)
            .ToList();
    }

    public async Task UpdateAsync(CarDto dto)
    {
        Validate(dto);

        Car car = EntityMapper.MapDtoToCar(dto);

        await this.unitOfWork.CarRepository.UpdateAsync(car);
        await this.unitOfWork.SaveAsync();
    }

    public void Validate(CarDto dto)
    {
        if (dto is null)
        {
            throw new ServiceException("Car dto is null.");
        }

        if (!Enum.IsDefined(typeof(CarType), dto.CarType) &&
            int.TryParse(dto.CarType, out int intType) && !Enum.IsDefined(typeof(CarType), intType))
        {
            throw new ArgumentException($"Car type [{dto.CarType}] does not exist.");
        }
    }
}
