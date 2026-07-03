using Data.Interfaces;

namespace Data.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly CarProductionDbContext context;
    private readonly ICarRepository carRepository;
    private readonly IManufacturerRepository manufacturerRepository;
    private readonly IServiceRepository serviceRepository;

    public UnitOfWork(
        CarProductionDbContext context, 
        ICarRepository carRepository, 
        IManufacturerRepository manufacturerRepository, 
        IServiceRepository serviceRepository)
    {
        this.context = context;
        this.carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        this.manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
        this.serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
    }

    public ICarRepository CarRepository { get => this.carRepository; }

    public IManufacturerRepository ManufacturerRepository { get => this.manufacturerRepository; }

    public IServiceRepository ServiceRepository { get => this.serviceRepository; }

    public async Task SaveAsync()
    {
        await this.context.SaveChangesAsync();
    }
}
