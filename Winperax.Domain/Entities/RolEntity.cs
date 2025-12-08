using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class RolEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string RolAdi { get; set; }
    public List<string> Yetkiler { get; set; } = new();
    public bool VarsayilanMi { get; set; }
}
