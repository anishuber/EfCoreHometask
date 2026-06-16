using Infrastructure.DTOs;

namespace Infrastructure.Interfaces;

public interface ICarService : IService<CarDto>
{
    Task<List<CarDto>> GetByManufacturerAsync(int manufacturerId);
}
