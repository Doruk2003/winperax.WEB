using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Winperax.API.Controllers.Finans
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateFinanceCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateFinanceCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteFinanceCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllFinanceQuery()));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id) =>
            Ok(await _mediator.Send(new GetFinanceByIdQuery(id)));
    }
}
