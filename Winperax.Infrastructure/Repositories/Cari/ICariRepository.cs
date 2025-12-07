using Winperax.Domain.Modules.Cari.Entities;
using System.Threading.Tasks;

namespace Winperax.Infrastructure.Repositories.Cari;

public interface I
{
    // Ornek metodlar (gerekirse degistirilebilir)
    Task<> GetByIdAsync(string id);
    Task<IEnumerable<>> GetAllAsync();
    Task AddAsync( entity);
    Task UpdateAsync( entity);
    Task DeleteAsync(string id);
}
