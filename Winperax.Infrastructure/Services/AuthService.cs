using System.Threading.Tasks;
using Winperax.Application.Features.Auth;

namespace Winperax.API.Services;

public class AuthService
{
    // Minimal placeholder service that keeps compilation happy.
    public Task<bool> AuthenticateAsync(LoginCommand cmd) => Task.FromResult(true);
    public Task<bool> RegisterAsync(RegisterCommand cmd) => Task.FromResult(true);
}
