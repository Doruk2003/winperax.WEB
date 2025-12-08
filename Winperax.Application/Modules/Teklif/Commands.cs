using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Teklif;

// CREATE
public record CreateTeklifCommand(
    string TeklifNo,
    string CariId,
    DateTime TeklifTarihi,
    DateTime GecerlilikTarihi,
    decimal ToplamTutar,
    string Durum
) : IRequest<TeklifEntity>;

public class CreateTeklifCommandHandler : IRequestHandler<CreateTeklifCommand, TeklifEntity>
{
    private readonly ITeklifRepository _repo;

    public CreateTeklifCommandHandler(ITeklifRepository repo)
    {
        _repo = repo;
    }

    public async Task<TeklifEntity> Handle(
        CreateTeklifCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new TeklifEntity
        {
            TeklifNo = request.TeklifNo,
            CariId = request.CariId,
            TeklifTarihi = request.TeklifTarihi,
            GecerlilikTarihi = request.GecerlilikTarihi,
            ToplamTutar = request.ToplamTutar,
            Durum = request.Durum,
        };

        await _repo.AddAsync(entity);
        return entity;
    }
}

// UPDATE
public record UpdateTeklifCommand(
    string Id,
    string TeklifNo,
    string CariId,
    DateTime TeklifTarihi,
    DateTime GecerlilikTarihi,
    decimal ToplamTutar,
    string Durum
) : IRequest<TeklifEntity>;

public class UpdateTeklifCommandHandler : IRequestHandler<UpdateTeklifCommand, TeklifEntity>
{
    private readonly ITeklifRepository _repo;

    public UpdateTeklifCommandHandler(ITeklifRepository repo)
    {
        _repo = repo;
    }

    public async Task<TeklifEntity> Handle(
        UpdateTeklifCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Teklif kaydı bulunamadı: " + request.Id);

        entity.TeklifNo = request.TeklifNo;
        entity.CariId = request.CariId;
        entity.TeklifTarihi = request.TeklifTarihi;
        entity.GecerlilikTarihi = request.GecerlilikTarihi;
        entity.ToplamTutar = request.ToplamTutar;
        entity.Durum = request.Durum;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteTeklifCommand(string Id) : IRequest<bool>;

public class DeleteTeklifCommandHandler : IRequestHandler<DeleteTeklifCommand, bool>
{
    private readonly ITeklifRepository _repo;

    public DeleteTeklifCommandHandler(ITeklifRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(
        DeleteTeklifCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Teklif kaydı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
