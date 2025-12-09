using FluentValidation;
using Winperax.Application.Modules.Bordro;

namespace Winperax.Application.Validators.Bordro
{
    public class DeleteBordroCommandValidator : AbstractValidator<DeleteBordroCommand>
    {
        public DeleteBordroCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Bordro ID boş olamaz.")
                .Length(1, 50).WithMessage("Bordro ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}