using Data.Entities;

namespace Data.Interfaces;

public interface ICarRepository : IRepository<Car>
{
    Task<List<Car>> GetByManufacturer(int manufacturerId);
}
