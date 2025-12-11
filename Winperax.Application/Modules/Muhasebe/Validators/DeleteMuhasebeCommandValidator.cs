using FluentValidation;
using Winperax.Application.Modules.Muhasebe;

namespace Winperax.Application.Validators.Muhasebe
{
    public class DeleteMuhasebeCommandValidator : AbstractValidator<DeleteMuhasebeCommand>
    {
        public DeleteMuhasebeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Muhasebe ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Muhasebe ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}
