using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class PersonelEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string TcKimlikNo { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public DateTime DogumTarihi { get; set; }
    public string Departman { get; set; }
    public string Pozisyon { get; set; }
    public DateTime IseGirisTarihi { get; set; }
    public DateTime? IstenCikisTarihi { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public string Adres { get; set; }
    public bool AktifMi { get; set; }
}
