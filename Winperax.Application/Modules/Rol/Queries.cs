using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;


namespace Winperax.Application.Modules.Rol;

// GET BY ID
public record GetRolByIdQuery(string Id) : IRequest<RolEntity>;

public class GetRolByIdQueryHandler : IRequestHandler<GetRolByIdQuery, RolEntity>
{
    private readonly IRolRepository _repo;

    public GetRolByIdQueryHandler(IRolRepository repo)
    {
        _repo = repo;
    }

    public async Task<RolEntity> Handle(
        GetRolByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllRolQuery() : IRequest<IEnumerable<RolEntity>>;

public class GetAllRolQueryHandler
    : IRequestHandler<GetAllRolQuery, IEnumerable<RolEntity>>
{
    private readonly IRolRepository _repo;

    public GetAllRolQueryHandler(IRolRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<RolEntity>> Handle(
        GetAllRolQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterRolQuery(string? Text) : IRequest<IEnumerable<RolEntity>>;

public class FilterRolQueryHandler
    : IRequestHandler<FilterRolQuery, IEnumerable<RolEntity>>
{
    private readonly IRolRepository _repo;

    public FilterRolQueryHandler(IRolRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<RolEntity>> Handle(
        FilterRolQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.RolAdi != null && x.RolAdi.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
        );
    }
}
