namespace Data.Interfaces;

public interface IRepository<T>
    where T : class
{
    Task AddAsync(T entity);

    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task DeleteAsync(T entity);

    Task DeleteByIdAsync(int id);

    Task UpdateAsync(T entity);
}