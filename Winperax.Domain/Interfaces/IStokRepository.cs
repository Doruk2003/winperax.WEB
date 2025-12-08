using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IStokRepository
{
    Task<StokEntity> GetByIdAsync(string id);
    Task<IEnumerable<StokEntity>> GetAllAsync();
    Task AddAsync(StokEntity entity);
    Task UpdateAsync(StokEntity entity);
    Task DeleteAsync(string id);
}
