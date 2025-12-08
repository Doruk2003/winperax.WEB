using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Modules.Finans.Entities;
using Winperax.Infrastructure.Persistence;

namespace Winperax.Infrastructure.Repositories.Finans;

public class FinansRepository : IFinansRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<FinansRepository> _logger;

    public FinansRepository(IMongoContext context, ILogger<FinansRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Finans> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<Finans>("Finanss");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Finans>> GetAllAsync()
    {
        var collection = _context.GetCollection<Finans>("Finanss");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Finans entity)
    {
        var collection = _context.GetCollection<Finans>("Finanss");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Finans entity)
    {
        var collection = _context.GetCollection<Finans>("Finanss");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<Finans>("Finanss");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
