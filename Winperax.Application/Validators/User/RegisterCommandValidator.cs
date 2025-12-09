using FluentValidation;
using Winperax.Application.Modules.User;

namespace Winperax.Application.Validators.User
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(150).WithMessage("E-posta adresi en fazla 150 karakter olabilir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.")
                .MaximumLength(100).WithMessage("Şifre en fazla 100 karakter olabilir.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Tam ad boş olamaz.")
                .MaximumLength(200).WithMessage("Tam ad en fazla 200 karakter olabilir.");
        }
    }
}