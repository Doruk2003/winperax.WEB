using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Winperax.API.Controllers.InsanKaynaklari
{
    [ApiController]
    [Route("api/[controller]")]
    public class HRController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HRController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateHRCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateHRCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteHRCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllHRQuery()));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
            => Ok(await _mediator.Send(new GetHRByIdQuery(id)));
    }
}
