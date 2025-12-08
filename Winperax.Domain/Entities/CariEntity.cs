using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class CariEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string CariKodu { get; set; }
    public string Unvan { get; set; }
    public string VergiNo { get; set; }
    public string VergiDairesi { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public string Adres { get; set; }
    public string Il { get; set; }
    public string Ilce { get; set; }
    public bool IsTedarikci { get; set; }
    public bool IsMusteri { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
