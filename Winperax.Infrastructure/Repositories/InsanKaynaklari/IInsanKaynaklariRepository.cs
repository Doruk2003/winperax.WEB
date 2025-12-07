using Winperax.Domain.Modules.InsanKaynaklari.Entities;
using System.Threading.Tasks;

namespace Winperax.Infrastructure.Repositories.InsanKaynaklari;

public interface I
{
    // Ornek metodlar (gerekirse degistirilebilir)
    Task<> GetByIdAsync(string id);
    Task<IEnumerable<>> GetAllAsync();
    Task AddAsync( entity);
    Task UpdateAsync( entity);
    Task DeleteAsync(string id);
}
