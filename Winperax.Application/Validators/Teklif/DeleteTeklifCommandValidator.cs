using FluentValidation;
using Winperax.Application.Modules.Teklif;

namespace Winperax.Application.Validators.Teklif
{
    public class DeleteTeklifCommandValidator : AbstractValidator<DeleteTeklifCommand>
    {
        public DeleteTeklifCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Teklif ID boş olamaz.")
                .Length(1, 50).WithMessage("Teklif ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}