using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> GetByIdAsync(string id);
    Task<IEnumerable<UserEntity>> GetAllAsync();
    Task AddAsync(UserEntity entity);
    Task UpdateAsync(UserEntity entity);
    Task DeleteAsync(string id);
    Task<UserEntity> GetByEmailAsync(string email);
}
