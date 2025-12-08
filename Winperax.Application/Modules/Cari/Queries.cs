using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Cari;

// GET BY ID
public record GetCariByIdQuery(string Id) : IRequest<CariEntity>;

public class GetCariByIdQueryHandler : IRequestHandler<GetCariByIdQuery, CariEntity>
{
    private readonly ICariRepository _repo;

    public GetCariByIdQueryHandler(ICariRepository repo)
    {
        _repo = repo;
    }

    public async Task<CariEntity> Handle(
        GetCariByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllCariQuery() : IRequest<IEnumerable<CariEntity>>;

public class GetAllCariQueryHandler : IRequestHandler<GetAllCariQuery, IEnumerable<CariEntity>>
{
    private readonly ICariRepository _repo;

    public GetAllCariQueryHandler(ICariRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CariEntity>> Handle(
        GetAllCariQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterCariQuery(string? Text) : IRequest<IEnumerable<CariEntity>>;

public class FilterCariQueryHandler : IRequestHandler<FilterCariQuery, IEnumerable<CariEntity>>
{
    private readonly ICariRepository _repo;

    public FilterCariQueryHandler(ICariRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CariEntity>> Handle(
        FilterCariQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.Unvan != null && x.Unvan.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.CariKodu != null
                && x.CariKodu.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.VergiNo != null
                && x.VergiNo.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}
