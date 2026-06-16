using Data.Interfaces;

namespace Data.Data;

public class UnitOfWork(
    CarProductionDbContext context, 
    ICarRepository carRepository, 
    IManufacturerRepository manufacturerRepository, 
    IServiceRepository serviceRepository) : IUnitOfWork
{
    private readonly CarProductionDbContext context = context;

    public ICarRepository CarRepository { get; } = carRepository;

    public IManufacturerRepository ManufacturerRepository { get; } = manufacturerRepository;

    public IServiceRepository ServiceRepository { get; } = serviceRepository;

    public async Task SaveAsync()
    {
        await this.context.SaveChangesAsync();
    }
}
