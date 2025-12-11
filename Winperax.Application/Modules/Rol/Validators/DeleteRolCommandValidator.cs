using FluentValidation;
using Winperax.Application.Modules.Rol;
using Winperax.Application.Modules.Rol.Commands.DeleteRol;

namespace Winperax.Application.Validators.Rol
{
    public class DeleteRolCommandValidator : AbstractValidator<DeleteRolCommand>
    {
        public DeleteRolCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Rol ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Rol ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}
