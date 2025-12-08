using System.Threading.Tasks;
using Winperax.Domain.Modules.Muhasebe.Entities;

namespace Winperax.Infrastructure.Repositories.Muhasebe;

public interface IMuhasebeRepository
{
    Task<Muhasebe> GetByIdAsync(string id);
    Task<IEnumerable<Muhasebe>> GetAllAsync();
    Task AddAsync(Muhasebe entity);
    Task UpdateAsync(Muhasebe entity);
    Task DeleteAsync(string id);
}
