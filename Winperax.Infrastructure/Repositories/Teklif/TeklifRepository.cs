using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Modules.Teklif.Entities;
using Winperax.Infrastructure.Persistence;

namespace Winperax.Infrastructure.Repositories.Teklif;

public class TeklifRepository : ITeklifRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<TeklifRepository> _logger;

    public TeklifRepository(IMongoContext context, ILogger<TeklifRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Teklif> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<Teklif>("Teklifler");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Teklif>> GetAllAsync()
    {
        var collection = _context.GetCollection<Teklif>("Teklifler");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Teklif entity)
    {
        var collection = _context.GetCollection<Teklif>("Teklifler");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Teklif entity)
    {
        var collection = _context.GetCollection<Teklif>("Teklifler");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<Teklif>("Teklifler");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
