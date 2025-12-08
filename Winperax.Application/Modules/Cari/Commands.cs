using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;

namespace Winperax.Application.Modules.Cari;

// CREATE
public record CreateCariCommand(string CariKodu, string Unvan, string VergiNo)
    : IRequest<CariEntity>;

public class CreateCariCommandHandler : IRequestHandler<CreateCariCommand, CariEntity>
{
    private readonly ICariRepository _repo;

    public CreateCariCommandHandler(ICariRepository repo)
    {
        _repo = repo;
    }

    public async Task<CariEntity> Handle(
        CreateCariCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new CariEntity
        {
            CariKodu = request.CariKodu,
            Unvan = request.Unvan,
            VergiNo = request.VergiNo,
        };

        await _repo.AddAsync(entity); // InsertAsync yerine AddAsync kullanıldı
        return entity;
    }
}

// UPDATE
public record UpdateCariCommand(string Id, string CariKodu, string Unvan, string VergiNo)
    : IRequest<CariEntity>;

public class UpdateCariCommandHandler : IRequestHandler<UpdateCariCommand, CariEntity>
{
    private readonly ICariRepository _repo;

    public UpdateCariCommandHandler(ICariRepository repo)
    {
        _repo = repo;
    }

    public async Task<CariEntity> Handle(
        UpdateCariCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Cari bulunamadı: " + request.Id);

        entity.CariKodu = request.CariKodu;
        entity.Unvan = request.Unvan;
        entity.VergiNo = request.VergiNo;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteCariCommand(string Id) : IRequest<bool>;

public class DeleteCariCommandHandler : IRequestHandler<DeleteCariCommand, bool>
{
    private readonly ICariRepository _repo;

    public DeleteCariCommandHandler(ICariRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteCariCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Cari bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
