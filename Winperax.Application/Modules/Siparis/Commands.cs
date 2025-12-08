using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Siparis;

// CREATE
public record CreateSiparisCommand(
    string SiparisNo,
    string CariId,
    DateTime SiparisTarihi,
    DateTime? TeslimTarihi,
    string Durum
) : IRequest<SiparisEntity>;

public class CreateSiparisCommandHandler : IRequestHandler<CreateSiparisCommand, SiparisEntity>
{
    private readonly ISiparisRepository _repo;

    public CreateSiparisCommandHandler(ISiparisRepository repo)
    {
        _repo = repo;
    }

    public async Task<SiparisEntity> Handle(
        CreateSiparisCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new SiparisEntity
        {
            SiparisNo = request.SiparisNo,
            CariId = request.CariId,
            SiparisTarihi = request.SiparisTarihi,
            TeslimTarihi = request.TeslimTarihi ?? DateTime.Now,
            Durum = request.Durum,
        };

        await _repo.AddAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteSiparisCommand(string Id) : IRequest<bool>;

public class DeleteSiparisCommandHandler : IRequestHandler<DeleteSiparisCommand, bool>
{
    private readonly ISiparisRepository _repo;

    public DeleteSiparisCommandHandler(ISiparisRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(
        DeleteSiparisCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Sipariş bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}

// UPDATE
public record UpdateSiparisCommand(
    string Id,
    string SiparisNo,
    string CariId,
    DateTime SiparisTarihi,
    DateTime? TeslimTarihi,
    string Durum
) : IRequest<SiparisEntity>;

public class UpdateSiparisCommandHandler : IRequestHandler<UpdateSiparisCommand, SiparisEntity>
{
    private readonly ISiparisRepository _repo;

    public UpdateSiparisCommandHandler(ISiparisRepository repo)
    {
        _repo = repo;
    }

    public async Task<SiparisEntity> Handle(
        UpdateSiparisCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Sipariş bulunamadı: " + request.Id);

        entity.SiparisNo = request.SiparisNo;
        entity.CariId = request.CariId;
        entity.SiparisTarihi = request.SiparisTarihi;
        entity.TeslimTarihi = request.TeslimTarihi ?? DateTime.Now;
        entity.Durum = request.Durum;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}