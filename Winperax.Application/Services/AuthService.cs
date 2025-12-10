using System.Threading.Tasks;
using Winperax.Application.Common.Responses; // ✅ AuthResponse için
using Winperax.Application.Services; // ✅ IPasswordHasher, IJwtTokenGenerator için (varsayalım)
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    // TODO: IPasswordHasher passwordHasher;
    // TODO: IJwtTokenGenerator jwtTokenGenerator;

    public AuthService(
        IUserRepository userRepository /*, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator*/
    )
    {
        _userRepository = userRepository;
        // this.passwordHasher = passwordHasher;
        // this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        // 1. Kullanıcıyı email'e göre getir
        // var user = await _userRepository.GetByEmailAsync(email); // Varsayalım bu metod var
        // if (user == null)
        // {
        //     return new AuthResponse { IsSuccess = false, Message = "Kullanıcı bulunamadı." };
        // }

        // 2. Parolayı doğrula
        // if (!passwordHasher.Verify(password, user.HashedPassword))
        // {
        //     return new AuthResponse { IsSuccess = false, Message = "Geçersiz parola." };
        // }

        // 3. JWT Token oluştur
        // var token = jwtTokenGenerator.GenerateToken(user);

        // 4. Başarılı yanıt döndür
        // return new AuthResponse { IsSuccess = true, Message = "Giriş başarılı.", Token = token };

        // TODO: Gerçek iş mantığı buraya gelecek
        return new AuthResponse
        {
            IsSuccess = false, // Varsayılan
            Message = "TODO: Gerçek giriş mantığı uygulanacak.",
            Token = "",
        };
    }

    public async Task<AuthResponse> RegisterAsync(string email, string password, string fullName)
    {
        // 1. Email zaten kullanımda mı kontrol et
        // var existingUser = await _userRepository.GetByEmailAsync(email);
        // if (existingUser != null)
        // {
        //     return new AuthResponse { IsSuccess = false, Message = "Email zaten kullanımda." };
        // }

        // 2. Parolayı hash'le
        // var hashedPassword = passwordHasher.Hash(password);

        // 3. Yeni kullanıcı oluştur ve veritabanına ekle
        // var newUser = new UserEntity { Email = email, HashedPassword = hashedPassword, FullName = fullName, ... };
        // await _userRepository.AddAsync(newUser);

        // 4. JWT Token oluştur (isteğe bağlı, bazen sadece kayıt yeterlidir)
        // var token = jwtTokenGenerator.GenerateToken(newUser);

        // 5. Başarılı yanıt döndür
        // return new AuthResponse { IsSuccess = true, Message = "Kayıt başarılı.", Token = token };

        // TODO: Gerçek iş mantığı buraya gelecek
        return new AuthResponse
        {
            IsSuccess = false, // Varsayılan
            Message = "TODO: Gerçek kayıt mantığı uygulanacak.",
            Token = "",
        };
    }

    // Eski metotlar, MediatR'a bağımlı olanlar. Yeni yapıya göre kullanılmayacaklar.
    // public async Task<bool> AuthenticateAsync(LoginCommand command)
    // {
    //     // ...
    // }

    // public async Task<bool> RegisterAsync(RegisterCommand command)
    // {
    //     // ...
    // }
}
