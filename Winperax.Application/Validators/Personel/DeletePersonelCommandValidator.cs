using FluentValidation;
using Winperax.Application.Modules.Personel;

namespace Winperax.Application.Validators.Personel
{
    public class DeletePersonelCommandValidator : AbstractValidator<DeletePersonelCommand>
    {
        public DeletePersonelCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Personel ID boş olamaz.")
                .Length(1, 50).WithMessage("Personel ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}