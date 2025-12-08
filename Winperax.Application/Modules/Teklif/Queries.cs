using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Teklif;

// GET BY ID
public record GetTeklifByIdQuery(string Id) : IRequest<TeklifEntity>;

public class GetTeklifByIdQueryHandler : IRequestHandler<GetTeklifByIdQuery, TeklifEntity>
{
    private readonly ITeklifRepository _repo;

    public GetTeklifByIdQueryHandler(ITeklifRepository repo)
    {
        _repo = repo;
    }

    public async Task<TeklifEntity> Handle(
        GetTeklifByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllTeklifQuery() : IRequest<IEnumerable<TeklifEntity>>;

public class GetAllTeklifQueryHandler
    : IRequestHandler<GetAllTeklifQuery, IEnumerable<TeklifEntity>>
{
    private readonly ITeklifRepository _repo;

    public GetAllTeklifQueryHandler(ITeklifRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TeklifEntity>> Handle(
        GetAllTeklifQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterTeklifQuery(string? Text) : IRequest<IEnumerable<TeklifEntity>>;

public class FilterTeklifQueryHandler
    : IRequestHandler<FilterTeklifQuery, IEnumerable<TeklifEntity>>
{
    private readonly ITeklifRepository _repo;

    public FilterTeklifQueryHandler(ITeklifRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TeklifEntity>> Handle(
        FilterTeklifQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.TeklifNo != null && x.TeklifNo.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.CariId != null
                && x.CariId.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.Durum != null
                && x.Durum.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}
