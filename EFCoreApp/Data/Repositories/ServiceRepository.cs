using Data.CustomExceptions;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly CarProductionDbContext context;

    public ServiceRepository(CarProductionDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(Service entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await this.context.Services.AddAsync(entity);
    }

    public async Task DeleteAsync(Service entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await this.DeleteByIdAsync(entity.Id);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var existing = await this.context.Services
            .FirstOrDefaultAsync(s => s.Id == id);

        if (existing is null)
        {
            throw new EntityNotFoundException<Service>(id);
        }

        this.context.Services.Remove(existing);
    }

    public async Task<List<Service>> GetAllAsync()
    {
        return await this.context.Services.ToListAsync();
    }

    public async Task<Service?> GetByIdAsync(int id)
    {
        return await this.context.Services.FindAsync(id);
    }

    public async Task UpdateAsync(Service entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var existing = await this.context.Services
            .FirstOrDefaultAsync(s => s.Id == entity.Id);

        if (existing is null)
        {
            throw new EntityNotFoundException<Service>(entity.Id);
        }

        this.context.Entry(existing).CurrentValues.SetValues(entity);
    }
}
