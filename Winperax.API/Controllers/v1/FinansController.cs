using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.Finans; // CreateFinansCommand, UpdateFinansCommand, DeleteFinansCommand, GetAllFinansQuery, GetFinansByIdQuery için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class FinansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllFinansQuery());
            return Ok(ApiResponse<object>.SuccessResult(result, "Finans records retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetFinansByIdQuery(id));
            return Ok(ApiResponse<object>.SuccessResult(result, "Finans record retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFinansCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
                ApiResponse<object>.SuccessResult(result, "Finans record created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateFinansCommand command)
        {
            var updatedCommand = new UpdateFinansCommand(
                id,
                command.EvrakNo,
                command.CariId,
                command.IslemTarihi,
                command.IslemTuru,
                command.Tutar,
                command.Aciklama,
                command.GirisMi
            );
            
            var result = await _mediator.Send(updatedCommand);
            return Ok(ApiResponse<object>.SuccessResult(result, "Finans record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteFinansCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResult(result, "Finans record deleted successfully"));
        }
    }
}