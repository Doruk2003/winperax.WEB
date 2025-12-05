using Microsoft.AspNetCore.Mvc;
using Winperax.Api.DTOs;
using Winperax.Api.Models;
using Winperax.Api.Services;

namespace Winperax.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var user = await _authService.AuthenticateAsync(dto.Username, dto.Password);
        if (user == null)
            return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı." });

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { token });
    }
}
