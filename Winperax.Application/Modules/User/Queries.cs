using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.User;

// GET BY ID
public record GetUserByIdQuery(string Id) : IRequest<UserEntity>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserEntity>
{
    private readonly IUserRepository _repo;

    public GetUserByIdQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserEntity> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllUsersQuery() : IRequest<IEnumerable<UserEntity>>;

public class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserEntity>>
{
    private readonly IUserRepository _repo;

    public GetAllUsersQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<UserEntity>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterUserQuery(string? Text) : IRequest<IEnumerable<UserEntity>>;

public class FilterUserQueryHandler
    : IRequestHandler<FilterUserQuery, IEnumerable<UserEntity>>
{
    private readonly IUserRepository _repo;

    public FilterUserQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<UserEntity>> Handle(
        FilterUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.Email != null && x.Email.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.KullaniciAdi != null // ✅ FullName yerine KullaniciAdi kullanıldı
                && x.KullaniciAdi.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}