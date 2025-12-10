using FluentValidation;
using Winperax.Application.Modules.InsanKaynaklari;

namespace Winperax.Application.Validators.InsanKaynaklari
{
    public class DeleteInsanKaynaklariCommandValidator
        : AbstractValidator<DeleteInsanKaynaklariCommand>
    {
        public DeleteInsanKaynaklariCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("İnsan Kaynakları ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("İnsan Kaynakları ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}
