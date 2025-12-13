using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class UserEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; } // âœ… required eklendi

    public required string KullaniciAdi { get; set; } // âœ… required eklendi
    public required string Email { get; set; } // âœ… required eklendi
    public required string PasswordHash { get; set; } // âœ… required eklendi
    public required string RolId { get; set; } // âœ… required eklendi
    public bool AktifMi { get; set; } // âœ… bool zaten null atanamaz ve deÄŸer tipi
    public DateTime CreatedAt { get; set; } // âœ… DateTime zaten null atanamaz ve deÄŸer tipi
}
