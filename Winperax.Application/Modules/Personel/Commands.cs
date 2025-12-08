using MediatR;
using Winperax.Domain.Entities;

namespace Winperax.Application.Modules.Personel;

// CREATE
public record CreatePersonelCommand(string Ad, string Soyad, string TcKimlikNo, string Departman)
    : IRequest<PersonelEntity>;

public class CreatePersonelCommandHandler : IRequestHandler<CreatePersonelCommand, PersonelEntity>
{
    private readonly IPersonelRepository _repo;

    public CreatePersonelCommandHandler(IPersonelRepository repo)
    {
        _repo = repo;
    }

    public async Task<PersonelEntity> Handle(
        CreatePersonelCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new PersonelEntity
        {
            Ad = request.Ad,
            Soyad = request.Soyad,
            TcKimlikNo = request.TcKimlikNo,
            Departman = request.Departman,
        };

        await _repo.AddAsync(entity); // InsertAsync yerine AddAsync kullanıldı
        return entity;
    }
}

// UPDATE
public record UpdatePersonelCommand(
    string Id,
    string Ad,
    string Soyad,
    string TcKimlikNo,
    string Departman
) : IRequest<PersonelEntity>;

public class UpdatePersonelCommandHandler : IRequestHandler<UpdatePersonelCommand, PersonelEntity>
{
    private readonly IPersonelRepository _repo;

    public UpdatePersonelCommandHandler(IPersonelRepository repo)
    {
        _repo = repo;
    }

    public async Task<PersonelEntity> Handle(
        UpdatePersonelCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Personel bulunamadı: " + request.Id);

        entity.Ad = request.Ad;
        entity.Soyad = request.Soyad;
        entity.TcKimlikNo = request.TcKimlikNo;
        entity.Departman = request.Departman;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeletePersonelCommand(string Id) : IRequest<bool>;

public class DeletePersonelCommandHandler : IRequestHandler<DeletePersonelCommand, bool>
{
    private readonly IPersonelRepository _repo;

    public DeletePersonelCommandHandler(IPersonelRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(
        DeletePersonelCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Personel bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
