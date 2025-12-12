using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.Muhasebe.Commands.CreateMuhasebe; // ✅ CreateMuhasebeCommand için
using Winperax.Application.Modules.Muhasebe.Commands.DeleteMuhasebe; // ✅ DeleteMuhasebeCommand için
using Winperax.Application.Modules.Muhasebe.Commands.UpdateMuhasebe; // ✅ UpdateMuhasebeCommand için
using Winperax.Application.Modules.Muhasebe.Queries.GetAllMuhasebes; // ✅ GetAllMuhasebeQuery için
using Winperax.Application.Modules.Muhasebe.Queries.GetMuhasebeById; // ✅ GetMuhasebeByIdQuery için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class MuhasebeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MuhasebeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllMuhasebeQuery());
            return Ok(
                ApiResponse<object>.SuccessResult(result, "Muhasebe records retrieved successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetMuhasebeByIdQuery(id));
            return Ok(
                ApiResponse<object>.SuccessResult(result, "Muhasebe record retrieved successfully")
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMuhasebeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                ApiResponse<object>.SuccessResult(result, "Muhasebe record created successfully")
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateMuhasebeCommand command)
        {
            var updatedCommand = new UpdateMuhasebeCommand(
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
            return Ok(
                ApiResponse<object>.SuccessResult(result, "Muhasebe record updated successfully")
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteMuhasebeCommand(id);
            var result = await _mediator.Send(command);
            return Ok(
                ApiResponse<object>.SuccessResult(result, "Muhasebe record deleted successfully")
            );
        }
    }
}
