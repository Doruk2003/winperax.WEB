using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class StokEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string StokKodu { get; set; }
    public required string StokAdi { get; set; }
    public required string Birim { get; set; }
    public decimal AlisFiyati { get; set; }
    public decimal SatisFiyati { get; set; }
    public decimal StokMiktari { get; set; }
    public required string Kategori { get; set; }
    public bool AktifMi { get; set; }
}

