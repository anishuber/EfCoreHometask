using Data.Entities;
using Data.Interfaces;
using Infrastructure.CustomExceptions;
using Infrastructure.DTOs;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ServiceService : IServiceService
{
    private readonly IUnitOfWork unitOfWork;

    public ServiceService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ServiceDto> AddAsync(ServiceDto dto)
    {
        var existingManufacturer = await this.unitOfWork.ServiceRepository.GetByIdAsync(dto.Id);

        if (existingManufacturer is not null)
        {
            throw new ArgumentException($"Service with id {dto.Id} already exists.");
        }

        var service = EntityMapper.MapDtoToService(dto);

        await this.unitOfWork.ServiceRepository.AddAsync(service);

        await unitOfWork.SaveAsync();

        return EntityMapper.MapServiceToDto(service);
    }

    public async Task DeleteAsync(int id)
    {
        await this.unitOfWork.ServiceRepository.DeleteByIdAsync(id);
        await this.unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<ServiceDto>> GetAllAsync()
    {
        IEnumerable<Service> services = await this.unitOfWork.ServiceRepository.GetAllAsync();

        return services
            .Select(EntityMapper.MapServiceToDto)
            .ToList();
    }

    public async Task<ServiceDto> GetByIdAsync(int id)
    {
        Service? service = await this.unitOfWork.ServiceRepository.GetByIdAsync(id);

        return service is null ?
            throw new ServiceException($"Service with ID {id} was not found") :
            EntityMapper.MapServiceToDto(service);
    }

    public async Task UpdateAsync(ServiceDto dto)
    {
        Validate(dto);

        Service service = EntityMapper.MapDtoToService(dto);

        await this.unitOfWork.ServiceRepository.UpdateAsync(service);
        await this.unitOfWork.SaveAsync();
    }

    public void Validate(ServiceDto dto)
    {
        if (dto is null)
        {
            throw new ServiceException("Service dto is null.");
        }
    }
}
