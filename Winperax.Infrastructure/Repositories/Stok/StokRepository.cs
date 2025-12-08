using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Modules.Stok.Entities;
using Winperax.Infrastructure.Persistence;

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

    public async Task<Stok> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<Stok>("Stoklar");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Stok>> GetAllAsync()
    {
        var collection = _context.GetCollection<Stok>("Stoklar");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Stok entity)
    {
        var collection = _context.GetCollection<Stok>("Stoklar");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Stok entity)
    {
        var collection = _context.GetCollection<Stok>("Stoklar");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<Stok>("Stoklar");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
