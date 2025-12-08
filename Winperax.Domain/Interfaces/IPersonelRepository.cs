using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IPersonelRepository
{
    Task<PersonelEntity> GetByIdAsync(string id);
    Task<IEnumerable<PersonelEntity>> GetAllAsync();
    Task AddAsync(PersonelEntity entity);
    Task UpdateAsync(PersonelEntity entity);
    Task DeleteAsync(string id);
}