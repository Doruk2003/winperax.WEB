using FluentValidation;
using Winperax.Application.Modules.User;
using Winperax.Application.Modules.User.Commands.DeleteUser;

namespace Winperax.Application.Validators.User
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Kullanıcı ID boş olamaz.")
                .Length(1, 50)
                .WithMessage("Kullanıcı ID 1 ile 50 karakter arasında olmalıdır.");
        }
    }
}
