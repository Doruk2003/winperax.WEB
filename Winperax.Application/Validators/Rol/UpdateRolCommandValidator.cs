using FluentValidation;
using Winperax.Application.Modules.Rol;

namespace Winperax.Application.Validators.Rol
{
    public class UpdateRolCommandValidator : AbstractValidator<UpdateRolCommand>
    {
        public UpdateRolCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Rol ID boş olamaz.")
                .Length(1, 50).WithMessage("Rol ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.RolAdi)
                .NotEmpty().WithMessage("Rol adı boş olamaz.")
                .MaximumLength(50).WithMessage("Rol adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Yetkiler)
                .Must(x => x != null).WithMessage("Rol için yetkiler tanımlanmalıdır.");

            RuleFor(x => x.VarsayilanMi)
                .NotNull().WithMessage("Varsayılan mı bilgisi boş olamaz.");
        }
    }
}