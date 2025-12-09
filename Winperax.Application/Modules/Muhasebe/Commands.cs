using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Muhasebe;

// CREATE
public record CreateMuhasebeCommand(
    string EvrakNo,
    string CariId,
    DateTime IslemTarihi,
    string IslemTuru,
    decimal Tutar,
    string Aciklama,
    bool GirisMi
) : IRequest<MuhasebeEntity>;

public class CreateMuhasebeCommandHandler : IRequestHandler<CreateMuhasebeCommand, MuhasebeEntity>
{
    private readonly IMuhasebeRepository _repo;

    public CreateMuhasebeCommandHandler(IMuhasebeRepository repo)
    {
        _repo = repo;
    }

    public async Task<MuhasebeEntity> Handle(
        CreateMuhasebeCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new MuhasebeEntity
        {
            EvrakNo = request.EvrakNo,
            CariId = request.CariId,
            IslemTarihi = request.IslemTarihi,
            IslemTuru = request.IslemTuru,
            Tutar = request.Tutar,
            Aciklama = request.Aciklama,
            GirisMi = request.GirisMi,
        };

        await _repo.AddAsync(entity);
        return entity;
    }
}

// UPDATE
public record UpdateMuhasebeCommand(
    string Id,
    string EvrakNo,
    string CariId,
    DateTime IslemTarihi,
    string IslemTuru,
    decimal Tutar,
    string Aciklama,
    bool GirisMi
) : IRequest<MuhasebeEntity>;

public class UpdateMuhasebeCommandHandler : IRequestHandler<UpdateMuhasebeCommand, MuhasebeEntity>
{
    private readonly IMuhasebeRepository _repo;

    public UpdateMuhasebeCommandHandler(IMuhasebeRepository repo)
    {
        _repo = repo;
    }

    public async Task<MuhasebeEntity> Handle(
        UpdateMuhasebeCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Muhasebe kaydı bulunamadı: " + request.Id);

        entity.EvrakNo = request.EvrakNo;
        entity.CariId = request.CariId;
        entity.IslemTarihi = request.IslemTarihi;
        entity.IslemTuru = request.IslemTuru;
        entity.Tutar = request.Tutar;
        entity.Aciklama = request.Aciklama;
        entity.GirisMi = request.GirisMi;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteMuhasebeCommand(string Id) : IRequest<bool>;

public class DeleteMuhasebeCommandHandler : IRequestHandler<DeleteMuhasebeCommand, bool>
{
    private readonly IMuhasebeRepository _repo;

    public DeleteMuhasebeCommandHandler(IMuhasebeRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(
        DeleteMuhasebeCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Muhasebe kaydı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
