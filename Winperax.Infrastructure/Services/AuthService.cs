using System.Threading.Tasks;
using Winperax.Application.Common.Responses; // ✅ AuthResponse için
using Winperax.Application.Services; // ✅ IAuthService, IPasswordHasher, IJwtTokenGenerator için
using Winperax.Domain.Interfaces; // ✅ IUserRepository için

namespace Winperax.Infrastructure.Services; // ✅ Namespace doğru

public class AuthService : IAuthService // ✅ Application katmanındaki arayüzü uyguluyor
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher; // ✅ Yeni bağımlılık
    private readonly IJwtTokenGenerator _jwtTokenGenerator; // ✅ Yeni bağımlılık

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher, // ✅ Constructor'a eklendi
        IJwtTokenGenerator jwtTokenGenerator // ✅ Constructor'a eklendi
    )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        // 1. Kullanıcıyı email'e göre getir
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            return new AuthResponse { IsSuccess = false, Message = "Kullanıcı bulunamadı." };
        }

        // 2. Parolayı doğrula
        if (!_passwordHasher.Verify(password, user.HashedPassword)) // ✅ Hash doğrulaması yapıldı
        {
            return new AuthResponse { IsSuccess = false, Message = "Geçersiz parola." };
        }

        // 3. JWT Token oluştur
        var token = _jwtTokenGenerator.GenerateToken(user); // ✅ Token üretildi

        // 4. Başarılı yanıt döndür
        return new AuthResponse { IsSuccess = true, Message = "Giriş başarılı.", Token = token };
    }

    public async Task<AuthResponse> RegisterAsync(string email, string password, string fullName)
    {
        // 1. Email zaten kullanımda mı kontrol et
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            return new AuthResponse { IsSuccess = false, Message = "Email zaten kullanımda." };
        }

        // 2. Parolayı hash'le
        var hashedPassword = _passwordHasher.Hash(password); // ✅ Parola hash'ledi

        // 3. Yeni kullanıcı oluştur ve veritabanına ekle
        var newUser = new UserEntity { Email = email, HashedPassword = hashedPassword, FullName = fullName, ... }; // ... diğer gerekli alanlar
        await _userRepository.AddAsync(newUser);

        // 4. JWT Token oluştur (isteğe bağlı)
        var token = _jwtTokenGenerator.GenerateToken(newUser); // ✅ Token üretildi

        // 5. Başarılı yanıt döndür
        return new AuthResponse { IsSuccess = true, Message = "Kayıt başarılı.", Token = token };
    }
}