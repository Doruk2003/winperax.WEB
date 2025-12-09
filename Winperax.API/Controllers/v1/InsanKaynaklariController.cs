using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.InsanKaynaklari; // CreateInsanKaynaklariCommand, UpdateInsanKaynaklariCommand, DeleteInsanKaynaklariCommand, GetAllInsanKaynaklariQuery, GetInsanKaynaklariByIdQuery için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class InsanKaynaklariController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InsanKaynaklariController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllInsanKaynaklariQuery());
            return Ok(ApiResponse<object>.SuccessResult(result, "Insan Kaynaklari records retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetInsanKaynaklariByIdQuery(id));
            return Ok(ApiResponse<object>.SuccessResult(result, "Insan Kaynaklari record retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInsanKaynaklariCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
                ApiResponse<object>.SuccessResult(result, "Insan Kaynaklari record created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateInsanKaynaklariCommand command)
        {
            var updatedCommand = new UpdateInsanKaynaklariCommand(
                id,
                command.PersonelId,
                command.YillikIzinHakki,
                command.KullanilanIzin,
                command.KalanIzin,
                command.Aciklama
            );
            
            var result = await _mediator.Send(updatedCommand);
            return Ok(ApiResponse<object>.SuccessResult(result, "Insan Kaynaklari record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteInsanKaynaklariCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResult(result, "Insan Kaynaklari record deleted successfully"));
        }
    }
}