namespace Winperax.Application.Abstractions.Repositories;

public interface IGenericRepository<T>
{
    Task<T> GetByIdAsync(string id);
    Task<List<T>> GetAllAsync();
    Task InsertAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
}
