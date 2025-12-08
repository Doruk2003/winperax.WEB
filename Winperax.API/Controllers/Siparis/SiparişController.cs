using MediatR;
using Microsoft.AspNetCore.Mvc;
using Winperax.Application.Modules.Siparis; // ✅ Bu satır eksikti

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
        public async Task<IActionResult> Create([FromBody] CreateSiparisCommand command) // ✅ CreateOrderCommand yerine CreateSiparisCommand
            => Ok(await _mediator.Send(command));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSiparisCommand command) // ✅ UpdateOrderCommand yerine UpdateSiparisCommand
            => Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteSiparisCommand command) // ✅ DeleteOrderCommand yerine DeleteSiparisCommand
            => Ok(await _mediator.Send(command));

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllSiparisQuery())); // ✅ GetAllOrderQuery yerine GetAllSiparisQuery

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id) =>
            Ok(await _mediator.Send(new GetSiparisByIdQuery(id))); // ✅ GetOrderByIdQuery yerine GetSiparisByIdQuery
    }
}