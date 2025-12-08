using System.Threading.Tasks;
using Winperax.Domain.Modules.InsanKaynaklari.Entities;

namespace Winperax.Infrastructure.Repositories.InsanKaynaklari;

public interface IInsanKaynaklariRepository
{
    Task<InsanKaynaklari> GetByIdAsync(string id);
    Task<IEnumerable<InsanKaynaklari>> GetAllAsync();
    Task AddAsync(InsanKaynaklari entity);
    Task UpdateAsync(InsanKaynaklari entity);
    Task DeleteAsync(string id);
}
