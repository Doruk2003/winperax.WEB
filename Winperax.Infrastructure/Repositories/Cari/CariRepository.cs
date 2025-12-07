using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Winperax.Domain.Modules.Cari.Entities;
using Winperax.Infrastructure.Persistence;

namespace Winperax.Infrastructure.Repositories.Cari;

public class  : I
{
    private readonly IMongoContext _context;
    private readonly ILogger<> _logger;

    public (IMongoContext context, ILogger<> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<>("Cari" + "s");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<>> GetAllAsync()
    {
        var collection = _context.GetCollection<>("Cari" + "s");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync( entity)
    {
        var collection = _context.GetCollection<>("Cari" + "s");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync( entity)
    {
        var collection = _context.GetCollection<>("Cari" + "s");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<>("Cari" + "s");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
