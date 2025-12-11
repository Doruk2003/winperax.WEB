using FluentValidation;
using Winperax.Application.Modules.Stok;
using Winperax.Application.Modules.Stok.Commands.CreateStok;

namespace Winperax.Application.Validators.Stok
{
    public class CreateStokCommandValidator : AbstractValidator<CreateStokCommand>
    {
        public CreateStokCommandValidator()
        {
            RuleFor(x => x.StokKodu)
                .NotEmpty()
                .WithMessage("Stok kodu boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Stok kodu en fazla 50 karakter olabilir.");

            RuleFor(x => x.StokAdi)
                .NotEmpty()
                .WithMessage("Stok adı boş olamaz.")
                .MaximumLength(200)
                .WithMessage("Stok adı en fazla 200 karakter olabilir.");

            RuleFor(x => x.Birim)
                .NotEmpty()
                .WithMessage("Birim boş olamaz.")
                .MaximumLength(20)
                .WithMessage("Birim en fazla 20 karakter olabilir.");

            RuleFor(x => x.AlisFiyati)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Alış fiyatı 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.SatisFiyati)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Satış fiyatı 0 veya daha büyük olmalıdır.")
                .GreaterThanOrEqualTo(x => x.AlisFiyati)
                .WithMessage("Satış fiyatı alış fiyatından düşük olamaz.");

            RuleFor(x => x.StokMiktari)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stok miktarı 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.Kategori)
                .NotEmpty()
                .WithMessage("Kategori boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Kategori en fazla 50 karakter olabilir.");
        }
    }
}
