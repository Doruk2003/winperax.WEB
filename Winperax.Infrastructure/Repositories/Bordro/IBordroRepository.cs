using System.Threading.Tasks;
using Winperax.Domain.Modules.Bordro.Entities;

namespace Winperax.Infrastructure.Repositories.Bordro;

public interface IBordroRepository
{
    Task<Bordro> GetByIdAsync(string id);
    Task<IEnumerable<Bordro>> GetAllAsync();
    Task AddAsync(Bordro entity);
    Task UpdateAsync(Bordro entity);
    Task DeleteAsync(string id);
}
