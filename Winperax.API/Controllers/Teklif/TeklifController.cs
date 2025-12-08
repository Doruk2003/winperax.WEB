using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Winperax.API.Controllers.Teklif
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOfferCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOfferCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOfferCommand command) =>
            Ok(await _mediator.Send(command));

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllOfferQuery()));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id) =>
            Ok(await _mediator.Send(new GetOfferByIdQuery(id)));
    }
}
