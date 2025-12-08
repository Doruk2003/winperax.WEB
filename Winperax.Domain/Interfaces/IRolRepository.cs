using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IRolRepository
{
    Task<RolEntity> GetByIdAsync(string id);
    Task<IEnumerable<RolEntity>> GetAllAsync();
    Task AddAsync(RolEntity entity);
    Task UpdateAsync(RolEntity entity);
    Task DeleteAsync(string id);
}