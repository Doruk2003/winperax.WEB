using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Winperax.Domain.ValueObjects;

namespace Winperax.Domain.Entities;

public class SiparisEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string SiparisNo { get; set; }
    public string CariId { get; set; }
    public DateTime SiparisTarihi { get; set; }
    public DateTime TeslimTarihi { get; set; }
    public List<SiparisKalemi> Kalemler { get; set; } = new();
    public decimal ToplamTutar { get; set; }
    public string Durum { get; set; }
}
