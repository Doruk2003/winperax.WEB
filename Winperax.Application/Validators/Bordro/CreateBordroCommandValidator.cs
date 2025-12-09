using FluentValidation;
using Winperax.Application.Modules.Bordro;

namespace Winperax.Application.Validators.Bordro
{
    public class CreateBordroCommandValidator : AbstractValidator<CreateBordroCommand>
    {
        public CreateBordroCommandValidator()
        {
            RuleFor(x => x.PersonelId)
                .NotEmpty().WithMessage("Personel ID boş olamaz.")
                .Length(1, 50).WithMessage("Personel ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Donem)
                .NotEmpty().WithMessage("Dönem boş olamaz.")
                .MaximumLength(20).WithMessage("Dönem en fazla 20 karakter olabilir.");

            RuleFor(x => x.BrutMaas)
                .GreaterThan(0).WithMessage("Brüt maaş 0'dan büyük olmalıdır.");

            RuleFor(x => x.NetMaas)
                .GreaterThanOrEqualTo(0).WithMessage("Net maaş 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.EkOdeme)
                .GreaterThanOrEqualTo(0).WithMessage("Ek ödeme 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.Kesinti)
                .GreaterThanOrEqualTo(0).WithMessage("Kesinti 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.ToplamOdenecek)
                .GreaterThanOrEqualTo(0).WithMessage("Toplam ödenecek miktar 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.OdemeTarihi)
                .LessThanOrEqualTo(DateTime.Now.AddMonths(1)).WithMessage("Ödeme tarihi gelecekte çok ileride olamaz.");
        }
    }
}