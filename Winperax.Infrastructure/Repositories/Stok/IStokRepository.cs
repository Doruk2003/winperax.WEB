using System.Threading.Tasks;
using Winperax.Domain.Modules.Stok.Entities;

namespace Winperax.Infrastructure.Repositories.Stok;

public interface IStokRepository
{
    Task<Stok> GetByIdAsync(string id);
    Task<IEnumerable<Stok>> GetAllAsync();
    Task AddAsync(Stok entity);
    Task UpdateAsync(Stok entity);
    Task DeleteAsync(string id);
}
