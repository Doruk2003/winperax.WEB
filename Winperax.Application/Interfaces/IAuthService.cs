using System.Threading.Tasks;
using Winperax.Application.Modules.User;

namespace Winperax.Application.Interfaces;

public interface IAuthService
{
    Task<bool> AuthenticateAsync(LoginCommand command);
    Task<bool> RegisterAsync(RegisterCommand command);
}