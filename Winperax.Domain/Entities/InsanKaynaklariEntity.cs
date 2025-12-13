using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Winperax.Domain.ValueObjects;

namespace Winperax.Domain.Entities;

public class InsanKaynaklariEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string PersonelId { get; set; }
    public int YillikIzinHakki { get; set; }
    public int KullanilanIzin { get; set; }
    public int KalanIzin { get; set; }
    public required List<IzinKaydi> Izinler { get; set; } = new(); // IDE0028 uyarÄ±sÄ±nÄ± gidermek iÃ§in
    public required string Aciklama { get; set; }
}

