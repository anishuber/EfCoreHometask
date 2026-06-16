namespace Data.Interfaces;

public interface IUnitOfWork
{
    public ICarRepository CarRepository { get; }

    public IManufacturerRepository ManufacturerRepository { get; }

    public IServiceRepository ServiceRepository { get; }

    Task SaveAsync();
}
