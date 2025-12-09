using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Finans;

// CREATE
public record CreateFinansCommand(
    string EvrakNo,
    string CariId,
    DateTime IslemTarihi,
    string IslemTuru,
    decimal Tutar,
    string Aciklama,
    bool GirisMi
) : IRequest<FinansEntity>;

public class CreateFinansCommandHandler : IRequestHandler<CreateFinansCommand, FinansEntity>
{
    private readonly IFinansRepository _repo;

    public CreateFinansCommandHandler(IFinansRepository repo)
    {
        _repo = repo;
    }

    public async Task<FinansEntity> Handle(
        CreateFinansCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new FinansEntity
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
public record UpdateFinansCommand(
    string Id,
    string EvrakNo,
    string CariId,
    DateTime IslemTarihi,
    string IslemTuru,
    decimal Tutar,
    string Aciklama,
    bool GirisMi
) : IRequest<FinansEntity>;

public class UpdateFinansCommandHandler : IRequestHandler<UpdateFinansCommand, FinansEntity>
{
    private readonly IFinansRepository _repo;

    public UpdateFinansCommandHandler(IFinansRepository repo)
    {
        _repo = repo;
    }

    public async Task<FinansEntity> Handle(
        UpdateFinansCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Finans kaydı bulunamadı: " + request.Id);

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
public record DeleteFinansCommand(string Id) : IRequest<bool>;

public class DeleteFinansCommandHandler : IRequestHandler<DeleteFinansCommand, bool>
{
    private readonly IFinansRepository _repo;

    public DeleteFinansCommandHandler(IFinansRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteFinansCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Finans kaydı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
