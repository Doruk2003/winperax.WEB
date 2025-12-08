using MediatR;
using Winperax.Domain.Entities;

namespace Winperax.Application.Modules.Siparis;

// CREATE
public record CreateSiparisCommand(
    string SiparisNo,
    string CariId,
    DateTime Tarih,
    DateTime? TeslimTarihi,
    string Durum,
    string? Aciklama
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
            Tarih = request.Tarih,
            TeslimTarihi = request.TeslimTarihi,
            Durum = request.Durum,
            Aciklama = request.Aciklama,
        };

        await _repo.AddAsync(entity); // Burada AddAsync kullanıldı
        return entity;
    }
}
