using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Modules.Bordro.Entities;
using Winperax.Infrastructure.Persistence;

namespace Winperax.Infrastructure.Repositories.Bordro;

public class BordroRepository : IBordroRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<BordroRepository> _logger;

    public BordroRepository(IMongoContext context, ILogger<BordroRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Bordro> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<Bordro>("Bordrolar");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Bordro>> GetAllAsync()
    {
        var collection = _context.GetCollection<Bordro>("Bordrolar");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Bordro entity)
    {
        var collection = _context.GetCollection<Bordro>("Bordrolar");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Bordro entity)
    {
        var collection = _context.GetCollection<Bordro>("Bordrolar");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<Bordro>("Bordrolar");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}
