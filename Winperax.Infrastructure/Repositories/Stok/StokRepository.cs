using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Entities;
using Winperax.Infrastructure.Persistence;
using Winperax.Domain.Interfaces;

namespace Winperax.Infrastructure.Repositories.Stok;

public class StokRepository : IStokRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<StokRepository> _logger;

    public StokRepository(IMongoContext context, ILogger<StokRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<StokEntity> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<StokEntity>("Stoklar");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<StokEntity>> GetAllAsync()
    {
        var collection = _context.GetCollection<StokEntity>("Stoklar");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(StokEntity entity)
    {
        var collection = _context.GetCollection<StokEntity>("Stoklar");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(StokEntity entity)
    {
        var collection = _context.GetCollection<StokEntity>("Stoklar");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<StokEntity>("Stoklar");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
