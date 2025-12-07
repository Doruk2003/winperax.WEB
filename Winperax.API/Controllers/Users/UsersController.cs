using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Winperax.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllUsersQuery()));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
            => Ok(await _mediator.Send(new GetUserByIdQuery(id)));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand command)
            => Ok(await _mediator.Send(command));
    }
}
