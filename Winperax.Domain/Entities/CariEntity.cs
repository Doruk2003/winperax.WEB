using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class CariEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; } // âœ… required eklendi

    public required string CariKodu { get; set; } // âœ… required eklendi
    public required string Unvan { get; set; } // âœ… required eklendi
    public required string VergiNo { get; set; } // âœ… required eklendi
    public required string VergiDairesi { get; set; } // âœ… required eklendi
    public required string Telefon { get; set; } // âœ… required eklendi
    public required string Email { get; set; } // âœ… required eklendi
    public required string Adres { get; set; } // âœ… required eklendi
    public required string Il { get; set; } // âœ… required eklendi
    public required string Ilce { get; set; } // âœ… required eklendi
    public bool IsTedarikci { get; set; } // âœ… bool zaten null atanamaz
    public bool IsMusteri { get; set; } // âœ… bool zaten null atanamaz

    public DateTime CreatedAt { get; set; } // âœ… DateTime zaten null atanamaz
    public DateTime? UpdatedAt { get; set; } // âœ… DateTime? null atanabilir
}
