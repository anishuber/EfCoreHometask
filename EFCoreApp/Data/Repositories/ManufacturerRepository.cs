using Data.CustomExceptions;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly CarProductionDbContext context;

    public ManufacturerRepository(CarProductionDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(Manufacturer entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await this.context.Manufacturers.AddAsync(entity);
    }

    public async Task DeleteAsync(Manufacturer entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await this.DeleteByIdAsync(entity.Id);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var existing = await this.context.Manufacturers
            .FirstOrDefaultAsync(m => m.Id == id);

        if (existing is null)
        {
            throw new EntityNotFoundException<Manufacturer>(id);
        }

        this.context.Manufacturers.Remove(existing);
    }

    public async Task<List<Manufacturer>> GetAllAsync()
    {
        return await this.context.Manufacturers.ToListAsync();
    }

    public async Task<Manufacturer?> GetByIdAsync(int id)
    {
        return await this.context.Manufacturers.FindAsync(id);
    }

    public async Task UpdateAsync(Manufacturer entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var existing = await this.context.Manufacturers
            .FirstOrDefaultAsync(m => m.Id == entity.Id);

        if (existing is null)
        {
            throw new EntityNotFoundException<Manufacturer>(entity.Id);
        }

        this.context.Entry(existing).CurrentValues.SetValues(entity);
    }
}
