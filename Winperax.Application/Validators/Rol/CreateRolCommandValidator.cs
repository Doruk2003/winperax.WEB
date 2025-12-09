using FluentValidation;
using Winperax.Application.Modules.Rol;

namespace Winperax.Application.Validators.Rol
{
    public class CreateRolCommandValidator : AbstractValidator<CreateRolCommand>
    {
        public CreateRolCommandValidator()
        {
            RuleFor(x => x.RolAdi)
                .NotEmpty().WithMessage("Rol adı boş olamaz.")
                .MaximumLength(50).WithMessage("Rol adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Yetkiler)
                .Must(x => x != null && x.Count() > 0).WithMessage("Rol için en az bir yetki tanımlanmalıdır.");

            RuleFor(x => x.VarsayilanMi)
                .NotNull().WithMessage("Varsayılan mı bilgisi boş olamaz.");
        }
    }
}