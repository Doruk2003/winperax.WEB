using System.Threading.Tasks;
using Winperax.Domain.Modules.Teklif.Entities;

namespace Winperax.Infrastructure.Repositories.Teklif;

public interface ITeklifRepository
{
    Task<Teklif> GetByIdAsync(string id);
    Task<IEnumerable<Teklif>> GetAllAsync();
    Task AddAsync(Teklif entity);
    Task UpdateAsync(Teklif entity);
    Task DeleteAsync(string id);
}
