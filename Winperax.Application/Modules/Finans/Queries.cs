using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Finans;

// GET BY ID
public record GetFinansByIdQuery(string Id) : IRequest<FinansEntity>;

public class GetFinansByIdQueryHandler : IRequestHandler<GetFinansByIdQuery, FinansEntity>
{
    private readonly IFinansRepository _repo;

    public GetFinansByIdQueryHandler(IFinansRepository repo)
    {
        _repo = repo;
    }

    public async Task<FinansEntity> Handle(
        GetFinansByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllFinansQuery() : IRequest<IEnumerable<FinansEntity>>;

public class GetAllFinansQueryHandler
    : IRequestHandler<GetAllFinansQuery, IEnumerable<FinansEntity>>
{
    private readonly IFinansRepository _repo;

    public GetAllFinansQueryHandler(IFinansRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<FinansEntity>> Handle(
        GetAllFinansQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterFinansQuery(string? Text) : IRequest<IEnumerable<FinansEntity>>;

public class FilterFinansQueryHandler
    : IRequestHandler<FilterFinansQuery, IEnumerable<FinansEntity>>
{
    private readonly IFinansRepository _repo;

    public FilterFinansQueryHandler(IFinansRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<FinansEntity>> Handle(
        FilterFinansQuery request,
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
