using FluentValidation;
using Winperax.Application.Modules.Cari;
using Winperax.Application.Modules.Cari.Commands.DeleteCari;

namespace Winperax.Application.Validators.Cari
{
    public class DeleteCariCommandValidator : AbstractValidator<DeleteCariCommand>
    {
        public DeleteCariCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Cari ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Cari ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}
