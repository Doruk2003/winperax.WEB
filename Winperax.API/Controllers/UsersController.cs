using Microsoft.AspNetCore.Mvc;
using Winperax.Api.Services;

namespace Winperax.Api.Controllers;

[ApiController]
[Route(""api/[controller]"")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(new { success = true, message = ""GetAllUsers endpoint çalışıyor"" });
    }
}
