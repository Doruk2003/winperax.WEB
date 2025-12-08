namespace Winperax.Domain.ValueObjects;

public class TeklifKalemi
{
    public string StokId { get; set; }
    public decimal Miktar { get; set; }
    public decimal Fiyat { get; set; }
    public decimal Tutar => Miktar * Fiyat;
}
