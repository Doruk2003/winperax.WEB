using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Winperax.API.Controllers.Stok
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateStockCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateStockCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteStockCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllStockQuery()));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
            => Ok(await _mediator.Send(new GetStockByIdQuery(id)));
    }
}
