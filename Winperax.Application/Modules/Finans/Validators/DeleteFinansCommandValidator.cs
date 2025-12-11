using FluentValidation;
using Winperax.Application.Modules.Finans;

namespace Winperax.Application.Validators.Finans
{
    public class DeleteFinansCommandValidator : AbstractValidator<DeleteFinansCommand>
    {
        public DeleteFinansCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Finans ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Finans ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}
