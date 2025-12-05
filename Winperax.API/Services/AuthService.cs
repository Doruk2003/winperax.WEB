using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Winperax.Api.Context;
using Winperax.Api.Models;
using Winperax.Api.Settings;

namespace Winperax.Api.Services;

public class AuthService
{
    private readonly MongoDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public AuthService(MongoDbContext context, IOptions<JwtSettings> options)
    {
        _context = context;
        _jwtSettings = options.Value;
    }

    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var hashed = HashPassword(password);
        return await _context
            .GetCollection<User>("Users")
            .Find(u => u.Username == username && u.PasswordHash == hashed)
            .FirstOrDefaultAsync();
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Username),
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
