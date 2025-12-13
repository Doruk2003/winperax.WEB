using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class RolEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string RolAdi { get; set; }
    public required List<string> Yetkiler { get; set; } = new();
    public bool VarsayilanMi { get; set; }
}

