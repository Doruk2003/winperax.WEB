using FluentValidation;
using Winperax.Application.Modules.AuditLog;

namespace Winperax.Application.Validators.AuditLog
{
    public class DeleteAuditLogCommandValidator : AbstractValidator<DeleteAuditLogCommand>
    {
        public DeleteAuditLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("AuditLog ID boş olamaz.")
                .Length(1, 50).WithMessage("AuditLog ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}