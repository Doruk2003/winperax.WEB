using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Winperax.Domain.ValueObjects;

namespace Winperax.Domain.Entities;

public class TeklifEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string TeklifNo { get; set; }
    public required string CariId { get; set; }
    public DateTime TeklifTarihi { get; set; }
    public DateTime GecerlilikTarihi { get; set; }
    public required List<TeklifKalemi> Kalemler { get; set; } = new();
    public decimal ToplamTutar { get; set; }
    public required string Durum { get; set; }
}
