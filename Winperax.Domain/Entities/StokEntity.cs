using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class StokEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string StokKodu { get; set; }
    public string StokAdi { get; set; }
    public string Birim { get; set; }
    public decimal AlisFiyati { get; set; }
    public decimal SatisFiyati { get; set; }
    public decimal StokMiktari { get; set; }
    public string Kategori { get; set; }
    public bool AktifMi { get; set; }
}
