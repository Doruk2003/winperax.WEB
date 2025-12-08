using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Bordro;

// GET BY ID
public record GetBordroByIdQuery(string Id) : IRequest<BordroEntity>;

public class GetBordroByIdQueryHandler : IRequestHandler<GetBordroByIdQuery, BordroEntity>
{
    private readonly IBordroRepository _repo;

    public GetBordroByIdQueryHandler(IBordroRepository repo)
    {
        _repo = repo;
    }

    public async Task<BordroEntity> Handle(
        GetBordroByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllBordroQuery() : IRequest<IEnumerable<BordroEntity>>;

public class GetAllBordroQueryHandler
    : IRequestHandler<GetAllBordroQuery, IEnumerable<BordroEntity>>
{
    private readonly IBordroRepository _repo;

    public GetAllBordroQueryHandler(IBordroRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<BordroEntity>> Handle(
        GetAllBordroQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterBordroQuery(string? Text) : IRequest<IEnumerable<BordroEntity>>;

public class FilterBordroQueryHandler
    : IRequestHandler<FilterBordroQuery, IEnumerable<BordroEntity>>
{
    private readonly IBordroRepository _repo;

    public FilterBordroQueryHandler(IBordroRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<BordroEntity>> Handle(
        FilterBordroQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.PersonelId != null && x.PersonelId.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.Donem != null
                && x.Donem.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}