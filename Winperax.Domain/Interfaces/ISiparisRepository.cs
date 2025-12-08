using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface ISiparisRepository
{
    Task<SiparisEntity> GetByIdAsync(string id);
    Task<IEnumerable<SiparisEntity>> GetAllAsync();
    Task AddAsync(SiparisEntity entity);
    Task UpdateAsync(SiparisEntity entity);
    Task DeleteAsync(string id);
}
