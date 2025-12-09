using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.Teklif; // CreateTeklifCommand, UpdateTeklifCommand, DeleteTeklifCommand, GetAllTeklifQuery, GetTeklifByIdQuery için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class TeklifController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeklifController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTeklifQuery());
            return Ok(ApiResponse<object>.SuccessResult(result, "Teklif records retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetTeklifByIdQuery(id));
            return Ok(ApiResponse<object>.SuccessResult(result, "Teklif record retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeklifCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
                ApiResponse<object>.SuccessResult(result, "Teklif record created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTeklifCommand command)
        {
            var updatedCommand = new UpdateTeklifCommand(
                id,
                command.TeklifNo,
                command.CariId,
                command.TeklifTarihi,
                command.GecerlilikTarihi,
                command.ToplamTutar,
                command.Durum
            );
            
            var result = await _mediator.Send(updatedCommand);
            return Ok(ApiResponse<object>.SuccessResult(result, "Teklif record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteTeklifCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResult(result, "Teklif record deleted successfully"));
        }
    }
}