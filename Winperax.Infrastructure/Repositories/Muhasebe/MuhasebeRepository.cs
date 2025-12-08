using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Modules.Muhasebe.Entities;
using Winperax.Infrastructure.Persistence;

namespace Winperax.Infrastructure.Repositories.Muhasebe;

public class MuhasebeRepository : IMuhasebeRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<MuhasebeRepository> _logger;

    public MuhasebeRepository(IMongoContext context, ILogger<MuhasebeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Muhasebe> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<Muhasebe>("Muhasebeler");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Muhasebe>> GetAllAsync()
    {
        var collection = _context.GetCollection<Muhasebe>("Muhasebeler");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Muhasebe entity)
    {
        var collection = _context.GetCollection<Muhasebe>("Muhasebeler");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Muhasebe entity)
    {
        var collection = _context.GetCollection<Muhasebe>("Muhasebeler");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<Muhasebe>("Muhasebeler");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
