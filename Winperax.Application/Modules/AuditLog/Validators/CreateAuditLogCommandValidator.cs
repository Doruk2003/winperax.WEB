using FluentValidation;
using Winperax.Application.Modules.AuditLog; // Eski using korunur (eğer Command sınıfı burada tanımlıysa, ama değil)
using Winperax.Application.Modules.AuditLog.Commands.CreateAuditLog; // ✅ Yeni eklenen using satırı
using Winperax.Application.Modules.AuditLog.Commands.CreateAuditLog;

namespace Winperax.Application.Validators.AuditLog // Veya Winperax.Application.Modules.AuditLog.Validators, hangisi doğruysa
{
    public class CreateAuditLogCommandValidator : AbstractValidator<CreateAuditLogCommand>
    {
        public CreateAuditLogCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("KullanÄ±cÄ± ID boÅŸ olamaz.")
                .Length(1, 50)
                .WithMessage("KullanÄ±cÄ± ID 1 ile 50 karakter arasÄ±nda olmalÄ±dÄ±r.");  

            RuleFor(x => x.EntityAdi)
                .NotEmpty()
                .WithMessage("Entity adÄ± boÅŸ olamaz.")
                .MaximumLength(100)
                .WithMessage("Entity adÄ± en fazla 100 karakter olabilir.");

            RuleFor(x => x.EntityId)
                .NotEmpty()
                .WithMessage("Entity ID boÅŸ olamaz.")
                .Length(1, 50)
                .WithMessage("Entity ID 1 ile 50 karakter arasÄ±nda olmalÄ±dÄ±r.");

            RuleFor(x => x.IslemTur)
                .NotEmpty()
                .WithMessage("Ä°ÅŸlem tÃ¼rÃ¼ boÅŸ olamaz.")
                .MaximumLength(20)
                .WithMessage("Ä°ÅŸlem tÃ¼rÃ¼ en fazla 20 karakter olabilir.");

            RuleFor(x => x.Tarih)
                .NotEmpty()
                .WithMessage("Tarih boÅŸ olamaz.")
                .LessThanOrEqualTo(DateTime.Now.AddMinutes(1))
                .WithMessage("Tarih gelecekte Ã§ok ileride olamaz.");

            RuleFor(x => x.Detay)
                .MaximumLength(500)
                .WithMessage("Detay en fazla 500 karakter olabilir.");
        }
    }
}
