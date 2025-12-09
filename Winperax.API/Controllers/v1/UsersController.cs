using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.User; // LoginCommand ve RegisterCommand için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(ApiResponse<object>.Success(result, "Users retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(ApiResponse<object>.Success(result, "User retrieved successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand command)
        {
            // Command sınıfında Id property'sine atama yapamayacağımız için, yeni bir instance oluşturuyoruz
            var updatedCommand = new UpdateUserCommand(
                id,
                command.KullaniciAdi,
                command.Email,
                command.RolId,
                command.AktifMi
            );

            var result = await _mediator.Send(updatedCommand);
            return Ok(ApiResponse<object>.Success(result, "User updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.Success(result, "User deleted successfully"));
        }
    }
}
