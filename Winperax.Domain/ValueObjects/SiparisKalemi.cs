namespace Winperax.Domain.ValueObjects;

public class SiparisKalemi
{
    public string StokId { get; set; }
    public string Birim { get; set; }
    public decimal Miktar { get; set; }
    public decimal Fiyat { get; set; }
    public decimal Tutar => Miktar * Fiyat;
}
