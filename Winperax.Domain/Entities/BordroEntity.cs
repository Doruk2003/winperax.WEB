using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class BordroEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string PersonelId { get; set; }
    public required string Donem { get; set; }
    public decimal BrutMaas { get; set; }
    public decimal NetMaas { get; set; }
    public decimal EkOdeme { get; set; }
    public decimal Kesinti { get; set; }
    public decimal ToplamOdenecek { get; set; }
    public DateTime OdemeTarihi { get; set; }
}
