using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Winperax.Domain.ValueObjects;

namespace Winperax.Domain.Entities;

public class SiparisEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string SiparisNo { get; set; }
    public required string CariId { get; set; }
    public DateTime SiparisTarihi { get; set; }
    public DateTime TeslimTarihi { get; set; }
    public required List<SiparisKalemi> Kalemler { get; set; } = new();
    public decimal ToplamTutar { get; set; }
    public required string Durum { get; set; }
}

