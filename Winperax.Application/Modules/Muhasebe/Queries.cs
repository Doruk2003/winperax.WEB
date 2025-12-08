using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Muhasebe;

// GET BY ID
public record GetMuhasebeByIdQuery(string Id) : IRequest<MuhasebeEntity>;

public class GetMuhasebeByIdQueryHandler : IRequestHandler<GetMuhasebeByIdQuery, MuhasebeEntity>
{
    private readonly IMuhasebeRepository _repo;

    public GetMuhasebeByIdQueryHandler(IMuhasebeRepository repo)
    {
        _repo = repo;
    }

    public async Task<MuhasebeEntity> Handle(
        GetMuhasebeByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllMuhasebeQuery() : IRequest<IEnumerable<MuhasebeEntity>>;

public class GetAllMuhasebeQueryHandler
    : IRequestHandler<GetAllMuhasebeQuery, IEnumerable<MuhasebeEntity>>
{
    private readonly IMuhasebeRepository _repo;

    public GetAllMuhasebeQueryHandler(IMuhasebeRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<MuhasebeEntity>> Handle(
        GetAllMuhasebeQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterMuhasebeQuery(string? Text) : IRequest<IEnumerable<MuhasebeEntity>>;

public class FilterMuhasebeQueryHandler
    : IRequestHandler<FilterMuhasebeQuery, IEnumerable<MuhasebeEntity>>
{
    private readonly IMuhasebeRepository _repo;

    public FilterMuhasebeQueryHandler(IMuhasebeRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<MuhasebeEntity>> Handle(
        FilterMuhasebeQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.EvrakNo != null && x.EvrakNo.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.CariId != null
                && x.CariId.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.IslemTuru != null
                && x.IslemTuru.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.Aciklama != null
                && x.Aciklama.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}