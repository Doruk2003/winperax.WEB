using FluentValidation;
using Winperax.Application.Modules.Cari;

namespace Winperax.Application.Validators.Cari
{
    public class UpdateCariCommandValidator : AbstractValidator<UpdateCariCommand>
    {
        public UpdateCariCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Cari ID boş olamaz.")
                .Length(1, 50).WithMessage("Cari ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.CariKodu)
                .NotEmpty().WithMessage("Cari kodu boş olamaz.")
                .MaximumLength(50).WithMessage("Cari kodu en fazla 50 karakter olabilir.");

            RuleFor(x => x.Unvan)
                .NotEmpty().WithMessage("Ünvan boş olamaz.")
                .MaximumLength(200).WithMessage("Ünvan en fazla 200 karakter olabilir.");

            RuleFor(x => x.VergiNo)
                .NotEmpty().WithMessage("Vergi no boş olamaz.")
                .MaximumLength(11).WithMessage("Vergi no en fazla 11 karakter olabilir.")
                .Matches(@"^\d{10,11}$").WithMessage("Vergi no sadece rakamlardan oluşmalı ve 10-11 karakter uzunluğunda olmalıdır.");
        }
    }
}