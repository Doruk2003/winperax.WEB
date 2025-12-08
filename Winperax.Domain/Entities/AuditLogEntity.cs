using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class AuditLogEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string UserId { get; set; }
    public string EntityAdi { get; set; }
    public string EntityId { get; set; }
    public string IslemTur { get; set; }
    public DateTime Tarih { get; set; }
    public string Detay { get; set; }
}
