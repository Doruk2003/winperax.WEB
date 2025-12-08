using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;


namespace Winperax.Application.Modules.AuditLog;

// CREATE
public record CreateAuditLogCommand(
    string UserId,
    string EntityAdi,
    string EntityId,
    string IslemTur,
    DateTime Tarih,
    string Detay
) : IRequest<AuditLogEntity>;

public class CreateAuditLogCommandHandler : IRequestHandler<CreateAuditLogCommand, AuditLogEntity>
{
    private readonly IAuditLogRepository _repo;

    public CreateAuditLogCommandHandler(IAuditLogRepository repo)
    {
        _repo = repo;
    }

    public async Task<AuditLogEntity> Handle(
        CreateAuditLogCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new AuditLogEntity
        {
            UserId = request.UserId,
            EntityAdi = request.EntityAdi,
            EntityId = request.EntityId,
            IslemTur = request.IslemTur,
            Tarih = request.Tarih,
            Detay = request.Detay,
        };

        await _repo.AddAsync(entity);
        return entity;
    }
}

// UPDATE
public record UpdateAuditLogCommand(
    string Id,
    string UserId,
    string EntityAdi,
    string EntityId,
    string IslemTur,
    DateTime Tarih,
    string Detay
) : IRequest<AuditLogEntity>;

public class UpdateAuditLogCommandHandler : IRequestHandler<UpdateAuditLogCommand, AuditLogEntity>
{
    private readonly IAuditLogRepository _repo;

    public UpdateAuditLogCommandHandler(IAuditLogRepository repo)
    {
        _repo = repo;
    }

    public async Task<AuditLogEntity> Handle(
        UpdateAuditLogCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("AuditLog kaydı bulunamadı: " + request.Id);

        entity.UserId = request.UserId;
        entity.EntityAdi = request.EntityAdi;
        entity.EntityId = request.EntityId;
        entity.IslemTur = request.IslemTur;
        entity.Tarih = request.Tarih;
        entity.Detay = request.Detay;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteAuditLogCommand(string Id) : IRequest<bool>;

public class DeleteAuditLogCommandHandler : IRequestHandler<DeleteAuditLogCommand, bool>
{
    private readonly IAuditLogRepository _repo;

    public DeleteAuditLogCommandHandler(IAuditLogRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(
        DeleteAuditLogCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("AuditLog kaydı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}