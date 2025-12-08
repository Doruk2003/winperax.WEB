using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IBordroRepository
{
    Task<BordroEntity> GetByIdAsync(string id);
    Task<IEnumerable<BordroEntity>> GetAllAsync();
    Task AddAsync(BordroEntity entity);
    Task UpdateAsync(BordroEntity entity);
    Task DeleteAsync(string id);
}