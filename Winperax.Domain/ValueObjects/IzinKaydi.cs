namespace Winperax.Domain.ValueObjects;

public class IzinKaydi
{
    public DateTime Baslangic { get; set; }
    public DateTime Bitis { get; set; }
    public required string Aciklama { get; set; }
}

