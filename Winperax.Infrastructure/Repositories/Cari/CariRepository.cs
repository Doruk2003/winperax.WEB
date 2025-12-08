using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Entities;
using Winperax.Infrastructure.Persistence;
using Winperax.Domain.Interfaces;

namespace Winperax.Infrastructure.Repositories.Cari;

public class CariRepository : ICariRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<CariRepository> _logger;

    public CariRepository(IMongoContext context, ILogger<CariRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CariEntity> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<CariEntity>("Caris");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CariEntity>> GetAllAsync()
    {
        var collection = _context.GetCollection<CariEntity>("Caris");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(CariEntity entity)
    {
        var collection = _context.GetCollection<CariEntity>("Caris");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(CariEntity entity)
    {
        var collection = _context.GetCollection<CariEntity>("Caris");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<CariEntity>("Caris");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}