using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface ICariRepository
{
    Task<CariEntity> GetByIdAsync(string id);
    Task<IEnumerable<CariEntity>> GetAllAsync();
    Task AddAsync(CariEntity entity);
    Task UpdateAsync(CariEntity entity);
    Task DeleteAsync(string id);
}
