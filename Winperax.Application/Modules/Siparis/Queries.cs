using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Siparis;

// GET BY ID
public record GetSiparisByIdQuery(string Id) : IRequest<SiparisEntity>;

public class GetSiparisByIdQueryHandler : IRequestHandler<GetSiparisByIdQuery, SiparisEntity>
{
    private readonly ISiparisRepository _repo;

    public GetSiparisByIdQueryHandler(ISiparisRepository repo)
    {
        _repo = repo;
    }

    public async Task<SiparisEntity> Handle(
        GetSiparisByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllSiparisQuery() : IRequest<IEnumerable<SiparisEntity>>;

public class GetAllSiparisQueryHandler
    : IRequestHandler<GetAllSiparisQuery, IEnumerable<SiparisEntity>>
{
    private readonly ISiparisRepository _repo;

    public GetAllSiparisQueryHandler(ISiparisRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<SiparisEntity>> Handle(
        GetAllSiparisQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterSiparisQuery(string? Text) : IRequest<IEnumerable<SiparisEntity>>;

public class FilterSiparisQueryHandler
    : IRequestHandler<FilterSiparisQuery, IEnumerable<SiparisEntity>>
{
    private readonly ISiparisRepository _repo;

    public FilterSiparisQueryHandler(ISiparisRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<SiparisEntity>> Handle(
        FilterSiparisQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.SiparisNo != null && x.SiparisNo.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
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
