using FluentValidation;
using Winperax.Application.Modules.Teklif;
using Winperax.Application.Modules.Teklif.Commands.UpdateTeklif;

namespace Winperax.Application.Validators.Teklif
{
    public class UpdateTeklifCommandValidator : AbstractValidator<UpdateTeklifCommand>
    {
        public UpdateTeklifCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Teklif ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Teklif ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.TeklifNo)
                .NotEmpty()
                .WithMessage("Teklif numarası boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Teklif numarası en fazla 50 karakter olabilir.");

            RuleFor(x => x.CariId)
                .NotEmpty()
                .WithMessage("Cari ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Cari ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.TeklifTarihi)
                .NotEmpty()
                .WithMessage("Teklif tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Now.AddDays(1))
                .WithMessage("Teklif tarihi gelecekte olamaz.");

            RuleFor(x => x.GecerlilikTarihi)
                .GreaterThanOrEqualTo(x => x.TeklifTarihi)
                .WithMessage("Geçerlilik tarihi teklif tarihinden önce olamaz.")
                .LessThanOrEqualTo(DateTime.Now.AddMonths(3))
                .WithMessage("Geçerlilik tarihi teklif tarihinden 3 aydan fazla ileride olamaz.");

            RuleFor(x => x.ToplamTutar)
                .GreaterThan(0)
                .WithMessage("Toplam tutar 0'dan büyük olmalıdır.");

            RuleFor(x => x.Durum)
                .NotEmpty()
                .WithMessage("Teklif durumu boş olamaz.")
                .MaximumLength(20)
                .WithMessage("Teklif durumu en fazla 20 karakter olabilir.");
        }
    }
}
