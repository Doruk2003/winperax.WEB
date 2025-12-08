using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Winperax.API.Controllers.Cari
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllCustomerQuery()));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id) =>
            Ok(await _mediator.Send(new GetCustomerByIdQuery(id)));
    }
}
