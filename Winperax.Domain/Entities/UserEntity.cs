using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class UserEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string KullaniciAdi { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string RolId { get; set; }
    public bool AktifMi { get; set; }
    public DateTime CreatedAt { get; set; }
}
