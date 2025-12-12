namespace Winperax.Infrastructure.Settings;

public class JwtSettings
{
    public required string Secret { get; set; } = string.Empty; // required anahtar kelimesi eklendi
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiryInMinutes { get; set; } = 60;
}
