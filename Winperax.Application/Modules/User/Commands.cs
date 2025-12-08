using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.User;

// LOGIN
public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    // AuthService'ten alınıp buraya entegre edilebilir
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Burada kullanıcı doğrulama işlemleri yapılır
        // Örnek:
        // var user = await _userService.ValidateUserAsync(request.Email, request.Password);
        // if (user == null) throw new Exception("Geçersiz kullanıcı adı veya şifre.");

        return new AuthResponse
        {
            IsSuccess = true,
            Message = "Giriş başarılı",
            Token = "TOKEN_BURAYA_GELECEK"
        };
    }
}

// REGISTER
public record RegisterCommand(string Email, string Password, string FullName) : IRequest<AuthResponse>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Burada kullanıcı kayıt işlemleri yapılır
        // Örnek:
        // var result = await _userService.CreateUserAsync(request.Email, request.Password, request.FullName);

        return new AuthResponse
        {
            IsSuccess = true,
            Message = "Kayıt başarılı",
            Token = "TOKEN_BURAYA_GELECEK"
        };
    }
}

// AUTH RESPONSE
public class AuthResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
}
