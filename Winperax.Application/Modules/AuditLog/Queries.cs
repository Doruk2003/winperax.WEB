using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;


namespace Winperax.Application.Modules.AuditLog;

// GET BY ID
public record GetAuditLogByIdQuery(string Id) : IRequest<AuditLogEntity>;

public class GetAuditLogByIdQueryHandler : IRequestHandler<GetAuditLogByIdQuery, AuditLogEntity>
{
    private readonly IAuditLogRepository _repo;

    public GetAuditLogByIdQueryHandler(IAuditLogRepository repo)
    {
        _repo = repo;
    }

    public async Task<AuditLogEntity> Handle(
        GetAuditLogByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}

// GET ALL
public record GetAllAuditLogQuery() : IRequest<IEnumerable<AuditLogEntity>>;

public class GetAllAuditLogQueryHandler
    : IRequestHandler<GetAllAuditLogQuery, IEnumerable<AuditLogEntity>>
{
    private readonly IAuditLogRepository _repo;

    public GetAllAuditLogQueryHandler(IAuditLogRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<AuditLogEntity>> Handle(
        GetAllAuditLogQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repo.GetAllAsync();
    }
}

// FILTER / SEARCH
public record FilterAuditLogQuery(string? Text) : IRequest<IEnumerable<AuditLogEntity>>;

public class FilterAuditLogQueryHandler
    : IRequestHandler<FilterAuditLogQuery, IEnumerable<AuditLogEntity>>
{
    private readonly IAuditLogRepository _repo;

    public FilterAuditLogQueryHandler(IAuditLogRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<AuditLogEntity>> Handle(
        FilterAuditLogQuery request,
        CancellationToken cancellationToken
    )
    {
        var list = await _repo.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Text))
            return list;

        return list.Where(x =>
            (x.UserId != null && x.UserId.Contains(request.Text, StringComparison.OrdinalIgnoreCase))
            || (
                x.EntityAdi != null
                && x.EntityAdi.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.IslemTur != null
                && x.IslemTur.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
            || (
                x.Detay != null
                && x.Detay.Contains(request.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}