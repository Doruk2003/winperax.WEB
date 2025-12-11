using FluentValidation;
using Winperax.Application.Modules.Muhasebe;
using Winperax.Application.Modules.Muhasebe.Commands.CreateMuhasebe;

namespace Winperax.Application.Validators.Muhasebe
{
    public class CreateMuhasebeCommandValidator : AbstractValidator<CreateMuhasebeCommand>
    {
        public CreateMuhasebeCommandValidator()
        {
            RuleFor(x => x.EvrakNo)
                .NotEmpty()
                .WithMessage("Evrağın numarası boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Evrağın numarası en fazla 50 karakter olabilir.");

            RuleFor(x => x.CariId)
                .NotEmpty()
                .WithMessage("Cari ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Cari ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.IslemTarihi)
                .NotEmpty()
                .WithMessage("İşlem tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Now.AddDays(1))
                .WithMessage("İşlem tarihi gelecekte olamaz.");

            RuleFor(x => x.IslemTuru)
                .NotEmpty()
                .WithMessage("İşlem türü boş olamaz.")
                .MaximumLength(50)
                .WithMessage("İşlem türü en fazla 50 karakter olabilir.");

            RuleFor(x => x.Tutar).GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500)
                .WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.GirisMi).NotNull().WithMessage("Giriş mi çıkışı mı bilgisi boş olamaz.");
        }
    }
}
