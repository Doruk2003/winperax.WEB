using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.User;

// LOGIN
public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken
    )
    {
        // Burada kullanıcı doğrulama işlemleri yapılır
        // Örnek:
        // var user = await _userService.ValidateUserAsync(request.Email, request.Password);
        // if (user == null) throw new Exception("Geçersiz kullanıcı adı veya şifre.");

        return new AuthResponse
        {
            IsSuccess = true,
            Message = "Giriş başarılı",
            Token = "TOKEN_BURAYA_GELECEK",
        };
    }
}

// REGISTER
public record RegisterCommand(string Email, string Password, string FullName)
    : IRequest<AuthResponse>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken
    )
    {
        // Burada kullanıcı kayıt işlemleri yapılır
        // Örnek:
        // var result = await _userService.CreateUserAsync(request.Email, request.Password, request.FullName);

        return new AuthResponse
        {
            IsSuccess = true,
            Message = "Kayıt başarılı",
            Token = "TOKEN_BURAYA_GELECEK",
        };
    }
}

// UPDATE
public record UpdateUserCommand(
    string Id,
    string KullaniciAdi,
    string Email,
    string RolId,
    bool AktifMi
) : IRequest<UserEntity>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserEntity>
{
    private readonly IUserRepository _repo;

    public UpdateUserCommandHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserEntity> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Kullanıcı bulunamadı: " + request.Id);

        entity.KullaniciAdi = request.KullaniciAdi;
        entity.Email = request.Email;
        entity.RolId = request.RolId;
        entity.AktifMi = request.AktifMi;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteUserCommand(string Id) : IRequest<bool>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repo;

    public DeleteUserCommandHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Kullanıcı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}

// AUTH RESPONSE
public class AuthResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
