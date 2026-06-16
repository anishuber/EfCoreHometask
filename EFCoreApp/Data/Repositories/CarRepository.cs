using Data.CustomExceptions;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Data.Repositories;

public class CarRepository : ICarRepository
{
    private readonly CarProductionDbContext context;

    public CarRepository(CarProductionDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(Car entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await this.context.Cars.AddAsync(entity);
    }

    public async Task DeleteAsync(Car entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await this.DeleteByIdAsync(entity.VanId);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var existing = await this.context.Cars
            .FirstOrDefaultAsync(c => c.VanId == id);

        if (existing is null)
        {
            throw new EntityNotFoundException<Car>(id);
        }

        this.context.Cars.Remove(existing);
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await this.context.Cars.ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        return await this.context.Cars.FindAsync(id);
    }

    public async Task<List<Car>> GetByManufacturer(int manufacturerId)
    {
        return await this.context.Cars
            .Where(c => c.ManufacturerId == manufacturerId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Car entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var existing = await this.context.Cars
            .FirstOrDefaultAsync(c => c.VanId == entity.VanId);

        if (existing is null)
        {
            throw new EntityNotFoundException<Car>(entity.VanId);
        }

        this.context.Entry(existing).CurrentValues.SetValues(entity);
    }
}
