using FluentValidation;
using Winperax.Application.Modules.Siparis;
using Winperax.Application.Modules.Siparis.Commands.CreateSiparis;

namespace Winperax.Application.Validators.Siparis
{
    public class CreateSiparisCommandValidator : AbstractValidator<CreateSiparisCommand>
    {
        public CreateSiparisCommandValidator()
        {
            RuleFor(x => x.SiparisNo)
                .NotEmpty()
                .WithMessage("Sipariş numarası boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Sipariş numarası en fazla 50 karakter olabilir.");

            RuleFor(x => x.CariId)
                .NotEmpty()
                .WithMessage("Cari ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Cari ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.SiparisTarihi)
                .NotEmpty()
                .WithMessage("Sipariş tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Now.AddDays(1))
                .WithMessage("Sipariş tarihi gelecekte olamaz.");

            RuleFor(x => x.TeslimTarihi)
                .GreaterThanOrEqualTo(x => x.SiparisTarihi)
                .WithMessage("Teslim tarihi sipariş tarihinden önce olamaz.")
                .LessThanOrEqualTo(DateTime.Now.AddMonths(6))
                .WithMessage("Teslim tarihi sipariş tarihinden 6 aydan fazla ileride olamaz.");

            RuleFor(x => x.Durum)
                .NotEmpty()
                .WithMessage("Sipariş durumu boş olamaz.")
                .MaximumLength(20)
                .WithMessage("Sipariş durumu en fazla 20 karakter olabilir.");
        }
    }
}
