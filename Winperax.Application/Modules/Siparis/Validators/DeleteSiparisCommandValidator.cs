using FluentValidation;
using Winperax.Application.Modules.Siparis;

namespace Winperax.Application.Validators.Siparis
{
    public class DeleteSiparisCommandValidator : AbstractValidator<DeleteSiparisCommand>
    {
        public DeleteSiparisCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sipariş ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Sipariş ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}
