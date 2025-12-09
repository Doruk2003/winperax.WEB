using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.InsanKaynaklari;

// CREATE
public record CreateInsanKaynaklariCommand(
    string PersonelId,
    int YillikIzinHakki,
    int KullanilanIzin,
    int KalanIzin,
    string Aciklama
) : IRequest<InsanKaynaklariEntity>;

public class CreateInsanKaynaklariCommandHandler
    : IRequestHandler<CreateInsanKaynaklariCommand, InsanKaynaklariEntity>
{
    private readonly IInsanKaynaklariRepository _repo;

    public CreateInsanKaynaklariCommandHandler(IInsanKaynaklariRepository repo)
    {
        _repo = repo;
    }

    public async Task<InsanKaynaklariEntity> Handle(
        CreateInsanKaynaklariCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new InsanKaynaklariEntity
        {
            PersonelId = request.PersonelId,
            YillikIzinHakki = request.YillikIzinHakki,
            KullanilanIzin = request.KullanilanIzin,
            KalanIzin = request.KalanIzin,
            Aciklama = request.Aciklama,
        };

        await _repo.AddAsync(entity);
        return entity;
    }
}

// UPDATE
public record UpdateInsanKaynaklariCommand(
    string Id,
    string PersonelId,
    int YillikIzinHakki,
    int KullanilanIzin,
    int KalanIzin,
    string Aciklama
) : IRequest<InsanKaynaklariEntity>;

public class UpdateInsanKaynaklariCommandHandler
    : IRequestHandler<UpdateInsanKaynaklariCommand, InsanKaynaklariEntity>
{
    private readonly IInsanKaynaklariRepository _repo;

    public UpdateInsanKaynaklariCommandHandler(IInsanKaynaklariRepository repo)
    {
        _repo = repo;
    }

    public async Task<InsanKaynaklariEntity> Handle(
        UpdateInsanKaynaklariCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("İnsan Kaynakları kaydı bulunamadı: " + request.Id);

        entity.PersonelId = request.PersonelId;
        entity.YillikIzinHakki = request.YillikIzinHakki;
        entity.KullanilanIzin = request.KullanilanIzin;
        entity.KalanIzin = request.KalanIzin;
        entity.Aciklama = request.Aciklama;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteInsanKaynaklariCommand(string Id) : IRequest<bool>;

public class DeleteInsanKaynaklariCommandHandler
    : IRequestHandler<DeleteInsanKaynaklariCommand, bool>
{
    private readonly IInsanKaynaklariRepository _repo;

    public DeleteInsanKaynaklariCommandHandler(IInsanKaynaklariRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(
        DeleteInsanKaynaklariCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("İnsan Kaynakları kaydı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
