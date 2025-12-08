using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;
using Winperax.Infrastructure.Persistence;
using Winperax.Domain.Interfaces;

namespace Winperax.Infrastructure.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly IMongoContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(IMongoContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserEntity> GetByIdAsync(string id)
    {
        var collection = _context.GetCollection<UserEntity>("Users");
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var collection = _context.GetCollection<UserEntity>("Users");
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(UserEntity entity)
    {
        var collection = _context.GetCollection<UserEntity>("Users");
        await collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(UserEntity entity)
    {
        var collection = _context.GetCollection<UserEntity>("Users");
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var collection = _context.GetCollection<UserEntity>("Users");
        await collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task<UserEntity> GetByEmailAsync(string email)
    {
        var collection = _context.GetCollection<UserEntity>("Users");
        return await collection.Find(x => x.Email == email).FirstOrDefaultAsync();
    }
}