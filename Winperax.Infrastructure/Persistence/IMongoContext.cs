using MongoDB.Driver;

namespace Winperax.Infrastructure.Persistence;

public interface IMongoContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}
