using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Bordro;

// CREATE
public record CreateBordroCommand(
    string PersonelId,
    string Donem,
    decimal BrutMaas,
    decimal NetMaas,
    decimal EkOdeme,
    decimal Kesinti,
    decimal ToplamOdenecek,
    DateTime OdemeTarihi
) : IRequest<BordroEntity>;

public class CreateBordroCommandHandler : IRequestHandler<CreateBordroCommand, BordroEntity>
{
    private readonly IBordroRepository _repo;

    public CreateBordroCommandHandler(IBordroRepository repo)
    {
        _repo = repo;
    }

    public async Task<BordroEntity> Handle(
        CreateBordroCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new BordroEntity
        {
            PersonelId = request.PersonelId,
            Donem = request.Donem,
            BrutMaas = request.BrutMaas,
            NetMaas = request.NetMaas,
            EkOdeme = request.EkOdeme,
            Kesinti = request.Kesinti,
            ToplamOdenecek = request.ToplamOdenecek,
            OdemeTarihi = request.OdemeTarihi,
        };

        await _repo.AddAsync(entity);
        return entity;
    }
}

// UPDATE
public record UpdateBordroCommand(
    string Id,
    string PersonelId,
    string Donem,
    decimal BrutMaas,
    decimal NetMaas,
    decimal EkOdeme,
    decimal Kesinti,
    decimal ToplamOdenecek,
    DateTime OdemeTarihi
) : IRequest<BordroEntity>;

public class UpdateBordroCommandHandler : IRequestHandler<UpdateBordroCommand, BordroEntity>
{
    private readonly IBordroRepository _repo;

    public UpdateBordroCommandHandler(IBordroRepository repo)
    {
        _repo = repo;
    }

    public async Task<BordroEntity> Handle(
        UpdateBordroCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Bordro kaydı bulunamadı: " + request.Id);

        entity.PersonelId = request.PersonelId;
        entity.Donem = request.Donem;
        entity.BrutMaas = request.BrutMaas;
        entity.NetMaas = request.NetMaas;
        entity.EkOdeme = request.EkOdeme;
        entity.Kesinti = request.Kesinti;
        entity.ToplamOdenecek = request.ToplamOdenecek;
        entity.OdemeTarihi = request.OdemeTarihi;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteBordroCommand(string Id) : IRequest<bool>;

public class DeleteBordroCommandHandler : IRequestHandler<DeleteBordroCommand, bool>
{
    private readonly IBordroRepository _repo;

    public DeleteBordroCommandHandler(IBordroRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteBordroCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Bordro kaydı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
