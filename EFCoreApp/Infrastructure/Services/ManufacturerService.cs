using Data.Entities;
using Data.Interfaces;
using Infrastructure.CustomExceptions;
using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using System.Reflection;

namespace Infrastructure.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly IUnitOfWork unitOfWork;

    public ManufacturerService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ManufacturerDto> AddAsync(ManufacturerDto dto)
    {
        Validate(dto);

        var existingManufacturer = await this.unitOfWork.ManufacturerRepository.GetByIdAsync(dto.Id);

        if (existingManufacturer is not null)
        {
            throw new ArgumentException($"Manufacturer with id {dto.Id} already exists.");
        }

        var manufacturer = EntityMapper.MapDtoToManufacturer(dto);

        await this.unitOfWork.ManufacturerRepository.AddAsync(manufacturer);

        await unitOfWork.SaveAsync();

        return EntityMapper.MapManufacturerToDto(manufacturer);
    }

    public async Task DeleteAsync(int id)
    {
        await this.unitOfWork.ManufacturerRepository.DeleteByIdAsync(id);
        await this.unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<ManufacturerDto>> GetAllAsync()
    {
        IEnumerable<Manufacturer> manufacturers = await this.unitOfWork.ManufacturerRepository.GetAllAsync();

        return manufacturers
            .Select(EntityMapper.MapManufacturerToDto)
            .ToList();
    }

    public async Task<ManufacturerDto> GetByIdAsync(int id)
    {
        Manufacturer? manufacturer = await this.unitOfWork.ManufacturerRepository.GetByIdAsync(id);

        return manufacturer is null ?
            throw new ServiceException($"Manufacturer with ID {id} was not found") :
            EntityMapper.MapManufacturerToDto(manufacturer);
    }

    public async Task UpdateAsync(ManufacturerDto dto)
    {
        Validate(dto);

        Manufacturer manufacturer = EntityMapper.MapDtoToManufacturer(dto);

        await this.unitOfWork.ManufacturerRepository.UpdateAsync(manufacturer);
        await this.unitOfWork.SaveAsync();
    }

    public void Validate(ManufacturerDto dto)
    {
        if (dto is null)
        {
            throw new ServiceException("Manufacturer dto is null.");
        }
    }
}
