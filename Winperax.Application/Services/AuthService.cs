using System.Threading.Tasks;
using MediatR;
using Winperax.Application.Interfaces;
using Winperax.Application.Modules.User; // ✅ LoginCommand ve RegisterCommand için

namespace Winperax.Application.Services;

public class AuthService : IAuthService
{
    private readonly IMediator _mediator;

    public AuthService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> AuthenticateAsync(LoginCommand command)
    {
        var response = await _mediator.Send(command);
        return response.IsSuccess;
    }

    public async Task<bool> RegisterAsync(RegisterCommand command)
    {
        var response = await _mediator.Send(command);
        return response.IsSuccess;
    }
}
