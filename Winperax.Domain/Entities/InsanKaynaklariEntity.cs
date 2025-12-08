using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Winperax.Domain.ValueObjects;

namespace Winperax.Domain.Entities;

public class InsanKaynaklariEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string PersonelId { get; set; }
    public int YillikIzinHakki { get; set; }
    public int KullanilanIzin { get; set; }
    public int KalanIzin { get; set; }
    public List<IzinKaydi> Izinler { get; set; } = new();
    public string Aciklama { get; set; }
}
