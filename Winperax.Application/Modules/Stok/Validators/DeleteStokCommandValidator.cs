using FluentValidation;
using Winperax.Application.Modules.Stok;
using Winperax.Application.Modules.Stok.Commands.DeleteStok;

namespace Winperax.Application.Validators.Stok
{
    public class DeleteStokCommandValidator : AbstractValidator<DeleteStokCommand>
    {
        public DeleteStokCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Stok ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Stok ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}
