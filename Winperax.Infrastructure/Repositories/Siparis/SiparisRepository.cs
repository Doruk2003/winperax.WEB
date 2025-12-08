using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Modules.Siparis.Entities;
using Winperax.Infrastructure.Persistence;

namespace Winperax.Infrastructure.Repositories.Siparis;

public class SiparisRepository : ISiparisRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<SiparisRepository> _logger;

    public SiparisRepository(IMongoContext context, ILogger<SiparisRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Siparis> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<Siparis>("Siparisler");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Siparis>> GetAllAsync()
    {
        var collection = _context.GetCollection<Siparis>("Siparisler");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Siparis entity)
    {
        var collection = _context.GetCollection<Siparis>("Siparisler");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Siparis entity)
    {
        var collection = _context.GetCollection<Siparis>("Siparisler");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<Siparis>("Siparisler");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
