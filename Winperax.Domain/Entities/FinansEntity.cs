using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class FinansEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string EvrakNo { get; set; }
    public required string CariId { get; set; }
    public DateTime IslemTarihi { get; set; }
    public required string IslemTuru { get; set; }
    public decimal Tutar { get; set; }
    public required string Aciklama { get; set; }
    public bool GirisMi { get; set; }
}

