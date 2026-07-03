using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Infrastructure.DTOs;

namespace Infrastructure.Services;

public class CarProductionService(
    CarProductionDbContext context,
    IUnitOfWork unitOfWork)
{
    private readonly CarProductionDbContext context = context;
    public IUnitOfWork UnitOfWork { get; set; } = unitOfWork;

    public async Task<CarDto> CreateCarWithManufacturerAsync(CarDto carDto, ManufacturerDto manufacturerDto)
    {
        await using var transaction = await this.context.Database.BeginTransactionAsync();

        try
        {
            var manufacturer = EntityMapper.MapDtoToManufacturer(manufacturerDto);

            await this.UnitOfWork.ManufacturerRepository.AddAsync(manufacturer);
            await this.UnitOfWork.SaveAsync();

            carDto.ManufacturerId = manufacturer.Id;

            var car = EntityMapper.MapDtoToCar(carDto);

            await this.UnitOfWork.CarRepository.AddAsync(car);
            await this.UnitOfWork.SaveAsync();

            await transaction.CommitAsync();

            return EntityMapper.MapCarToDto(car);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
