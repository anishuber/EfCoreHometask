namespace Infrastructure.Interfaces;

public interface IService<TDto>
    where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();

    Task<TDto> GetByIdAsync(int id);

    Task<TDto> AddAsync(TDto dto);

    Task UpdateAsync(TDto dto);

    Task DeleteAsync(int id);

    void Validate(TDto dto);
}
