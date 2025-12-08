using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface ITeklifRepository
{
    Task<TeklifEntity> GetByIdAsync(string id);
    Task<IEnumerable<TeklifEntity>> GetAllAsync();
    Task AddAsync(TeklifEntity entity);
    Task UpdateAsync(TeklifEntity entity);
    Task DeleteAsync(string id);
}
