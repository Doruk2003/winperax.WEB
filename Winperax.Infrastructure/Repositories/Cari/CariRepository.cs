using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Modules.Cari.Entities;
using Winperax.Infrastructure.Persistence;

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

    public async Task<Cari> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<Cari>("Caris");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Cari>> GetAllAsync()
    {
        var collection = _context.GetCollection<Cari>("Caris");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Cari entity)
    {
        var collection = _context.GetCollection<Cari>("Caris");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Cari entity)
    {
        var collection = _context.GetCollection<Cari>("Caris");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<Cari>("Caris");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
