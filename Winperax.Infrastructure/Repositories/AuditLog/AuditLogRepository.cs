using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;
using Winperax.Infrastructure.Persistence;

namespace Winperax.Infrastructure.Repositories.AuditLog;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<AuditLogRepository> _logger;

    public AuditLogRepository(IMongoContext context, ILogger<AuditLogRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AuditLogEntity> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<AuditLogEntity>("AuditLogs");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<AuditLogEntity>> GetAllAsync()
    {
        var collection = _context.GetCollection<AuditLogEntity>("AuditLogs");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(AuditLogEntity entity)
    {
        var collection = _context.GetCollection<AuditLogEntity>("AuditLogs");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(AuditLogEntity entity)
    {
        var collection = _context.GetCollection<AuditLogEntity>("AuditLogs");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<AuditLogEntity>("AuditLogs");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}