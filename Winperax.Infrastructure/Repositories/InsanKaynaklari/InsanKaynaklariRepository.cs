using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Entities;
using Winperax.Infrastructure.Persistence;
using Winperax.Domain.Interfaces;

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

    public async Task<InsanKaynaklariEntity> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<InsanKaynaklariEntity>("InsanKaynaklaris");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<InsanKaynaklariEntity>> GetAllAsync()
    {
        var collection = _context.GetCollection<InsanKaynaklariEntity>("InsanKaynaklaris");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(InsanKaynaklariEntity entity)
    {
        var collection = _context.GetCollection<InsanKaynaklariEntity>("InsanKaynaklaris");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(InsanKaynaklariEntity entity)
    {
        var collection = _context.GetCollection<InsanKaynaklariEntity>("InsanKaynaklaris");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<InsanKaynaklariEntity>("InsanKaynaklaris");
        await collection.DeleteOneAsync(x => x.Id == id);
    }
}