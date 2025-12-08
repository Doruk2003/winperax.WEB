using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IMuhasebeRepository
{
    Task<MuhasebeEntity> GetByIdAsync(string id);
    Task<IEnumerable<MuhasebeEntity>> GetAllAsync();
    Task AddAsync(MuhasebeEntity entity);
    Task UpdateAsync(MuhasebeEntity entity);
    Task DeleteAsync(string id);
}
