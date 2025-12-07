using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Winperax.API.Controllers.Siparis
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOrderCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllOrderQuery()));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
            => Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
    }
}
