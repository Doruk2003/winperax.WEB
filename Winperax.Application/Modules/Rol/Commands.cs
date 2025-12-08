using MediatR;
using Winperax.Domain.Entities;
using Winperax.Domain.Interfaces;


namespace Winperax.Application.Modules.Rol;

// CREATE
public record CreateRolCommand(
    string RolAdi,
    List<string> Yetkiler,
    bool VarsayilanMi
) : IRequest<RolEntity>;

public class CreateRolCommandHandler : IRequestHandler<CreateRolCommand, RolEntity>
{
    private readonly IRolRepository _repo;

    public CreateRolCommandHandler(IRolRepository repo)
    {
        _repo = repo;
    }

    public async Task<RolEntity> Handle(
        CreateRolCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new RolEntity
        {
            RolAdi = request.RolAdi,
            Yetkiler = request.Yetkiler,
            VarsayilanMi = request.VarsayilanMi,
        };

        await _repo.AddAsync(entity);
        return entity;
    }
}

// UPDATE
public record UpdateRolCommand(
    string Id,
    string RolAdi,
    List<string> Yetkiler,
    bool VarsayilanMi
) : IRequest<RolEntity>;

public class UpdateRolCommandHandler : IRequestHandler<UpdateRolCommand, RolEntity>
{
    private readonly IRolRepository _repo;

    public UpdateRolCommandHandler(IRolRepository repo)
    {
        _repo = repo;
    }

    public async Task<RolEntity> Handle(
        UpdateRolCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Rol kaydı bulunamadı: " + request.Id);

        entity.RolAdi = request.RolAdi;
        entity.Yetkiler = request.Yetkiler;
        entity.VarsayilanMi = request.VarsayilanMi;

        await _repo.UpdateAsync(entity);
        return entity;
    }
}

// DELETE
public record DeleteRolCommand(string Id) : IRequest<bool>;

public class DeleteRolCommandHandler : IRequestHandler<DeleteRolCommand, bool>
{
    private readonly IRolRepository _repo;

    public DeleteRolCommandHandler(IRolRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(
        DeleteRolCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            throw new Exception("Rol kaydı bulunamadı: " + request.Id);

        await _repo.DeleteAsync(request.Id);
        return true;
    }
}
