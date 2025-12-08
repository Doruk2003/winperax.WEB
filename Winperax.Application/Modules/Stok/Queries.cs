using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Stok;

// GET BY ID
public record GetStokByIdQuery(string Id) : IRequest<StokEntity>;

public class GetStokByIdQueryHandler : IRequestHandler<GetStokByIdQuery, StokEntity>
{
    private readonly IStokRepository _repo;

    public GetStokByIdQueryHandler(IStokRepository repo)
    {
        _repo = repo;
    }

    public async Task<StokEntity> Handle(
        GetStokByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllStokQuery() : IRequest<IEnumerable<StokEntity>>;

public class GetAllStokQueryHandler
    : IRequestHandler<GetAllStokQuery, IEnumerable<StokEntity>>
{
    private readonly IStokRepository _repo;

    public GetAllStokQueryHandler(IStokRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<StokEntity>> Handle(
        GetAllStokQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterStokQuery(string? Text) : IRequest<IEnumerable<StokEntity>>;

public class FilterStokQueryHandler
    : IRequestHandler<FilterStokQuery, IEnumerable<StokEntity>>
{
    private readonly IStokRepository _repo;

    public FilterStokQueryHandler(IStokRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<StokEntity>> Handle(
        FilterStokQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.StokKodu != null && x.StokKodu.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.StokAdi != null
                && x.StokAdi.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.Kategori != null
                && x.Kategori.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}
