using FluentValidation;
using Winperax.Application.Modules.InsanKaynaklari;

namespace Winperax.Application.Validators.InsanKaynaklari
{
    public class UpdateInsanKaynaklariCommandValidator
        : AbstractValidator<UpdateInsanKaynaklariCommand>
    {
        public UpdateInsanKaynaklariCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("İnsan Kaynakları ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("İnsan Kaynakları ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.PersonelId)
                .NotEmpty()
                .WithMessage("Personel ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Personel ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.YillikIzinHakki)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Yıllık izin hakkı 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.KullanilanIzin)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Kullanılan izin 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.KalanIzin)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Kalan izin 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500)
                .WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
