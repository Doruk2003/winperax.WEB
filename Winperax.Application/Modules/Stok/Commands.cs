using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Stok;

// CREATE
public record CreateStokCommand(
    string StokKodu,
    string StokAdi,
    string Birim,
    decimal AlisFiyati,
    decimal SatisFiyati,
    decimal StokMiktari,
    string Kategori,
    bool AktifMi
) : IRequest<StokEntity>;

public class CreateStokCommandHandler : IRequestHandler<CreateStokCommand, StokEntity>
{
    private readonly IStokRepository _repo;

    public CreateStokCommandHandler(IStokRepository repo)
    {
        _repo = repo;
    }

    public async Task<StokEntity> Handle(
        CreateStokCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new StokEntity
        {
            StokKodu = request.StokKodu,
            StokAdi = request.StokAdi,
            Birim = request.Birim,
            AlisFiyati = request.AlisFiyati,
            SatisFiyati = request.SatisFiyati,
            StokMiktari = request.StokMiktari,
            Kategori = request.Kategori,
            AktifMi = request.AktifMi,
        };

        await _repo.AddAsync(entity);
        return entity;
    }
}

// UPDATE
public record UpdateStokCommand(
    string Id,
    string StokKodu,
    string StokAdi,
    string Birim,
    decimal AlisFiyati,
    decimal SatisFiyati,
    decimal StokMiktari,
    string Kategori,
    bool AktifMi
) : IRequest<StokEntity>;

public class UpdateStokCommandHandler : IRequestHandler<UpdateStokCommand, StokEntity>
{
    private readonly IStokRepository _repo;

    public UpdateStokCommandHandler(IStokRepository repo)
    {
        _repo = repo;
    }

    public async Task<StokEntity> Handle(
        UpdateStokCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Stok kaydı bulunamadı: " + request.Id);

        entity.StokKodu = request.StokKodu;
        entity.StokAdi = request.StokAdi;
        entity.Birim = request.Birim;
        entity.AlisFiyati = request.AlisFiyati;
        entity.SatisFiyati = request.SatisFiyati;
        entity.StokMiktari = request.StokMiktari;
        entity.Kategori = request.Kategori;
        entity.AktifMi = request.AktifMi;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteStokCommand(string Id) : IRequest<bool>;

public class DeleteStokCommandHandler : IRequestHandler<DeleteStokCommand, bool>
{
    private readonly IStokRepository _repo;

    public DeleteStokCommandHandler(IStokRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteStokCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Stok kaydı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
