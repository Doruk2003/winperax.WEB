using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class AuditLogEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string UserId { get; set; }
    public required string EntityAdi { get; set; }
    public required string EntityId { get; set; }
    public required string IslemTur { get; set; }
    public DateTime Tarih { get; set; }
    public required string Detay { get; set; }
}

