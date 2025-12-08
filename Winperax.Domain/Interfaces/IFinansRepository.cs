using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IFinansRepository
{
    Task<FinansEntity> GetByIdAsync(string id);
    Task<IEnumerable<FinansEntity>> GetAllAsync();
    Task AddAsync(FinansEntity entity);
    Task UpdateAsync(FinansEntity entity);
    Task DeleteAsync(string id);
}
