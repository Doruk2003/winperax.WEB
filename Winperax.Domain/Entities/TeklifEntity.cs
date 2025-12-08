using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Winperax.Domain.ValueObjects;

namespace Winperax.Domain.Entities;

public class TeklifEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string TeklifNo { get; set; }
    public string CariId { get; set; }
    public DateTime TeklifTarihi { get; set; }
    public DateTime GecerlilikTarihi { get; set; }
    public List<TeklifKalemi> Kalemler { get; set; } = new();
    public decimal ToplamTutar { get; set; }
    public string Durum { get; set; }
}
