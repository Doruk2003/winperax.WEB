using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IInsanKaynaklariRepository
{
    Task<InsanKaynaklariEntity> GetByIdAsync(string id);
    Task<IEnumerable<InsanKaynaklariEntity>> GetAllAsync();
    Task AddAsync(InsanKaynaklariEntity entity);
    Task UpdateAsync(InsanKaynaklariEntity entity);
    Task DeleteAsync(string id);
}
