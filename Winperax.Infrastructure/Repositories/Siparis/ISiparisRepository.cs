using System.Threading.Tasks;
using Winperax.Domain.Modules.Siparis.Entities;

namespace Winperax.Infrastructure.Repositories.Siparis;

public interface ISiparisRepository
{
    Task<Siparis> GetByIdAsync(string id);
    Task<IEnumerable<Siparis>> GetAllAsync();
    Task AddAsync(Siparis entity);
    Task UpdateAsync(Siparis entity);
    Task DeleteAsync(string id);
}
