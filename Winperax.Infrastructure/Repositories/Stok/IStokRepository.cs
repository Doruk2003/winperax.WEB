using Winperax.Domain.Modules.Stok.Entities;
using System.Threading.Tasks;

namespace Winperax.Infrastructure.Repositories.Stok;

public interface I
{
    // Ornek metodlar (gerekirse degistirilebilir)
    Task<> GetByIdAsync(string id);
    Task<IEnumerable<>> GetAllAsync();
    Task AddAsync( entity);
    Task UpdateAsync( entity);
    Task DeleteAsync(string id);
}
