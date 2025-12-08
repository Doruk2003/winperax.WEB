using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Modules.InsanKaynaklari.Entities;
using Winperax.Infrastructure.Persistence;

namespace Winperax.Infrastructure.Repositories.InsanKaynaklari;

public class InsanKaynaklariRepository : IInsanKaynaklariRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<InsanKaynaklariRepository> _logger;

    public InsanKaynaklariRepository(
        IMongoContext context,
        ILogger<InsanKaynaklariRepository> logger
    )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InsanKaynaklari> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<InsanKaynaklari>("InsanKaynaklaris");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<InsanKaynaklari>> GetAllAsync()
    {
        var collection = _context.GetCollection<InsanKaynaklari>("InsanKaynaklaris");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(InsanKaynaklari entity)
    {
        var collection = _context.GetCollection<InsanKaynaklari>("InsanKaynaklaris");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(InsanKaynaklari entity)
    {
        var collection = _context.GetCollection<InsanKaynaklari>("InsanKaynaklaris");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<InsanKaynaklari>("InsanKaynaklaris");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
