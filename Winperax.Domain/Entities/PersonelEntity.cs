using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class PersonelEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; } // âœ… required eklendi

    public required string TcKimlikNo { get; set; } // âœ… required eklendi
    public required string Ad { get; set; } // âœ… required eklendi
    public required string Soyad { get; set; } // âœ… required eklendi
    public DateTime DogumTarihi { get; set; } // âœ… DateTime, zaten null atanamaz
    public required string Departman { get; set; } // âœ… required eklendi
    public required string Pozisyon { get; set; } // âœ… required eklendi
    public DateTime IseGirisTarihi { get; set; } // âœ… DateTime, zaten null atanamaz
    public DateTime? IstenCikisTarihi { get; set; } // âœ… DateTime?, null atanabilir
    public required string Telefon { get; set; } // âœ… required eklendi
    public required string Email { get; set; } // âœ… required eklendi
    public required string Adres { get; set; } // âœ… required eklendi
    public bool AktifMi { get; set; } // âœ… bool, zaten null atanamaz
}
