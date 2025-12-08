using System.Threading.Tasks;
using Winperax.Domain.Entities;

namespace Winperax.Domain.Interfaces;

public interface IAuditLogRepository
{
    Task<AuditLogEntity> GetByIdAsync(string id);
    Task<IEnumerable<AuditLogEntity>> GetAllAsync();
    Task AddAsync(AuditLogEntity entity);
    Task UpdateAsync(AuditLogEntity entity);
    Task DeleteAsync(string id);
}