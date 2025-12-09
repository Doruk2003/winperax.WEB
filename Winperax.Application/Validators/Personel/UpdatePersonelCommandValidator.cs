using FluentValidation;
using Winperax.Application.Modules.Personel;

namespace Winperax.Application.Validators.Personel
{
    public class UpdatePersonelCommandValidator : AbstractValidator<UpdatePersonelCommand>
    {
        public UpdatePersonelCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Personel ID boş olamaz.")
                .Length(1, 50).WithMessage("Personel ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Personel adı boş olamaz.")
                .MaximumLength(100).WithMessage("Personel adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Soyad)
                .NotEmpty().WithMessage("Personel soyadı boş olamaz.")
                .MaximumLength(100).WithMessage("Personel soyadı en fazla 100 karakter olabilir.");

            RuleFor(x => x.TcKimlikNo)
                .NotEmpty().WithMessage("TC Kimlik Numarası boş olamaz.")
                .Matches(@"^\d{11}$").WithMessage("TC Kimlik Numarası sadece rakamlardan oluşmalı ve 11 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Departman)
                .NotEmpty().WithMessage("Departman boş olamaz.")
                .MaximumLength(50).WithMessage("Departman en fazla 50 karakter olabilir.");
        }
    }
}