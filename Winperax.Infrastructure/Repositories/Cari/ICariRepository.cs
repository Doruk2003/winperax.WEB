using System.Threading.Tasks;
using Winperax.Domain.Modules.Cari.Entities;

namespace Winperax.Infrastructure.Repositories.Cari;

public interface ICariRepository
{
    Task<Cari> GetByIdAsync(string id);
    Task<IEnumerable<Cari>> GetAllAsync();
    Task AddAsync(Cari entity);
    Task UpdateAsync(Cari entity);
    Task DeleteAsync(string id);
}
