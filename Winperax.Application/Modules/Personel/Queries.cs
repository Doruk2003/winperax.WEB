using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Personel;

// GET BY ID
public record GetPersonelByIdQuery(string Id) : IRequest<PersonelEntity>;

public class GetPersonelByIdQueryHandler : IRequestHandler<GetPersonelByIdQuery, PersonelEntity>
{
    private readonly IPersonelRepository _repo;

    public GetPersonelByIdQueryHandler(IPersonelRepository repo)
    {
        _repo = repo;
    }

    public async Task<PersonelEntity> Handle(
        GetPersonelByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllPersonelQuery() : IRequest<IEnumerable<PersonelEntity>>;

public class GetAllPersonelQueryHandler
    : IRequestHandler<GetAllPersonelQuery, IEnumerable<PersonelEntity>>
{
    private readonly IPersonelRepository _repo;

    public GetAllPersonelQueryHandler(IPersonelRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<PersonelEntity>> Handle(
        GetAllPersonelQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterPersonelQuery(string? Text) : IRequest<IEnumerable<PersonelEntity>>;

public class FilterPersonelQueryHandler
    : IRequestHandler<FilterPersonelQuery, IEnumerable<PersonelEntity>>
{
    private readonly IPersonelRepository _repo;

    public FilterPersonelQueryHandler(IPersonelRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<PersonelEntity>> Handle(
        FilterPersonelQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.Ad != null && x.Ad.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.Soyad != null
                && x.Soyad.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.TcKimlikNo != null
                && x.TcKimlikNo.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.Departman != null
                && x.Departman.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}
