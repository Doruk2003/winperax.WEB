using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Winperax.API.Controllers.Muhasebe
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAccountingCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountingCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAccountingCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllAccountingQuery()));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id) =>
            Ok(await _mediator.Send(new GetAccountingByIdQuery(id)));
    }
}
