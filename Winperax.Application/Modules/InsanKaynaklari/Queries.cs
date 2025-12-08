using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.InsanKaynaklari;

// GET BY ID
public record GetInsanKaynaklariByIdQuery(string Id) : IRequest<InsanKaynaklariEntity>;

public class GetInsanKaynaklariByIdQueryHandler : IRequestHandler<GetInsanKaynaklariByIdQuery, InsanKaynaklariEntity>
{
    private readonly IInsanKaynaklariRepository _repo;

    public GetInsanKaynaklariByIdQueryHandler(IInsanKaynaklariRepository repo)
    {
        _repo = repo;
    }

    public async Task<InsanKaynaklariEntity> Handle(
        GetInsanKaynaklariByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllInsanKaynaklariQuery() : IRequest<IEnumerable<InsanKaynaklariEntity>>;

public class GetAllInsanKaynaklariQueryHandler
    : IRequestHandler<GetAllInsanKaynaklariQuery, IEnumerable<InsanKaynaklariEntity>>
{
    private readonly IInsanKaynaklariRepository _repo;

    public GetAllInsanKaynaklariQueryHandler(IInsanKaynaklariRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<InsanKaynaklariEntity>> Handle(
        GetAllInsanKaynaklariQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterInsanKaynaklariQuery(string? Text) : IRequest<IEnumerable<InsanKaynaklariEntity>>;

public class FilterInsanKaynaklariQueryHandler
    : IRequestHandler<FilterInsanKaynaklariQuery, IEnumerable<InsanKaynaklariEntity>>
{
    private readonly IInsanKaynaklariRepository _repo;

    public FilterInsanKaynaklariQueryHandler(IInsanKaynaklariRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<InsanKaynaklariEntity>> Handle(
        FilterInsanKaynaklariQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.PersonelId != null && x.PersonelId.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.Aciklama != null
                && x.Aciklama.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}
