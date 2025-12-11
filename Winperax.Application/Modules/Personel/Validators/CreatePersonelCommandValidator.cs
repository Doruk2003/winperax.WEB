using FluentValidation;
using Winperax.Application.Modules.Personel;
using Winperax.Application.Modules.Personel.Commands.CreatePersonel;

namespace Winperax.Application.Validators.Personel
{
    public class CreatePersonelCommandValidator : AbstractValidator<CreatePersonelCommand>
    {
        public CreatePersonelCommandValidator()
        {
            RuleFor(x => x.Ad)
                .NotEmpty()
                .WithMessage("Personel adı boş olamaz.")
                .MaximumLength(100)
                .WithMessage("Personel adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Soyad)
                .NotEmpty()
                .WithMessage("Personel soyadı boş olamaz.")
                .MaximumLength(100)
                .WithMessage("Personel soyadı en fazla 100 karakter olabilir.");

            RuleFor(x => x.TcKimlikNo)
                .NotEmpty()
                .WithMessage("TC Kimlik Numarası boş olamaz.")
                .Matches(@"^\d{11}$")
                .WithMessage(
                    "TC Kimlik Numarası sadece rakamlardan oluşmalı ve 11 karakter uzunluğunda olmalıdır."
                );

            RuleFor(x => x.Departman)
                .NotEmpty()
                .WithMessage("Departman boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Departman en fazla 50 karakter olabilir.");
        }
    }
}
