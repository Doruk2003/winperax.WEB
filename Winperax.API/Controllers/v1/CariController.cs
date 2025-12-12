using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
// Eski using satırı kaldırıldı
using Winperax.Application.Modules.Cari.Commands.CreateCari; // ✅ CreateCariCommand için
using Winperax.Application.Modules.Cari.Commands.DeleteCari; // ✅ DeleteCariCommand için
using Winperax.Application.Modules.Cari.Commands.UpdateCari; // ✅ UpdateCariCommand için
using Winperax.Application.Modules.Cari.Queries.GetAllCari; // ✅ GetAllCariQuery için
using Winperax.Application.Modules.Cari.Queries.GetCariById; // ✅ GetCariByIdQuery için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class CariController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CariController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCariQuery());
            return Ok(
                ApiResponse<object>.SuccessResult(result, "Cari records retrieved successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetCariByIdQuery(id));
            return Ok(
                ApiResponse<object>.SuccessResult(result, "Cari record retrieved successfully")
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCariCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                ApiResponse<object>.SuccessResult(result, "Cari record created successfully")
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCariCommand command)
        {
            var updatedCommand = new UpdateCariCommand(
                id,
                command.CariKodu,
                command.Unvan,
                command.VergiNo
            );

            var result = await _mediator.Send(updatedCommand);
            return Ok(
                ApiResponse<object>.SuccessResult(result, "Cari record updated successfully")
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteCariCommand(id);
            var result = await _mediator.Send(command);
            return Ok(
                ApiResponse<object>.SuccessResult(result, "Cari record deleted successfully")
            );
        }
    }
}
