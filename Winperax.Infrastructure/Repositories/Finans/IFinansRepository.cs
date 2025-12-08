using System.Threading.Tasks;
using Winperax.Domain.Modules.Finans.Entities;

namespace Winperax.Infrastructure.Repositories.Finans;

public interface IFinansRepository
{
    Task<Finans> GetByIdAsync(string id);
    Task<IEnumerable<Finans>> GetAllAsync();
    Task AddAsync(Finans entity);
    Task UpdateAsync(Finans entity);
    Task DeleteAsync(string id);
}
