using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class FinansEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string EvrakNo { get; set; }
    public string CariId { get; set; }
    public DateTime IslemTarihi { get; set; }
    public string IslemTuru { get; set; }
    public decimal Tutar { get; set; }
    public string Aciklama { get; set; }
    public bool GirisMi { get; set; }
}
