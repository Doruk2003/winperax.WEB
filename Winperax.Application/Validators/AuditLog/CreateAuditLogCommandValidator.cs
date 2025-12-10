using FluentValidation;
using Winperax.Application.Modules.AuditLog;

namespace Winperax.Application.Validators.AuditLog
{
    public class CreateAuditLogCommandValidator : AbstractValidator<CreateAuditLogCommand>
    {
        public CreateAuditLogCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Kullanıcı ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Kullanıcı ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.EntityAdi)
                .NotEmpty()
                .WithMessage("Entity adı boş olamaz.")
                .MaximumLength(100)
                .WithMessage("Entity adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.EntityId)
                .NotEmpty()
                .WithMessage("Entity ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Entity ID 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.IslemTur)
                .NotEmpty()
                .WithMessage("İşlem türü boş olamaz.")
                .MaximumLength(20)
                .WithMessage("İşlem türü en fazla 20 karakter olabilir.");

            RuleFor(x => x.Tarih)
                .NotEmpty()
                .WithMessage("Tarih boş olamaz.")
                .LessThanOrEqualTo(DateTime.Now.AddMinutes(1))
                .WithMessage("Tarih gelecekte çok ileride olamaz.");

            RuleFor(x => x.Detay)
                .MaximumLength(500)
                .WithMessage("Detay en fazla 500 karakter olabilir.");
        }
    }
}
