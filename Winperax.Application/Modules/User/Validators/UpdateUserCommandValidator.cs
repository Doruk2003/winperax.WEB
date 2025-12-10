using FluentValidation;
using Winperax.Application.Modules.User;

namespace Winperax.Application.Validators.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Kullanıcı ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Kullanıcı ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.KullaniciAdi)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(150)
                .WithMessage("E-posta adresi en fazla 150 karakter olabilir.");

            RuleFor(x => x.RolId)
                .NotEmpty()
                .WithMessage("Rol ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Rol ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.AktifMi).NotNull().WithMessage("Aktif mi bilgisi boş olamaz.");
        }
    }
}
