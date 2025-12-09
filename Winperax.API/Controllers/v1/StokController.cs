using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.Stok; // CreateStokCommand, UpdateStokCommand, DeleteStokCommand, GetAllStokQuery, GetStokByIdQuery için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class StokController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StokController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllStokQuery());
            return Ok(ApiResponse<object>.SuccessResult(result, "Stok records retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetStokByIdQuery(id));
            return Ok(ApiResponse<object>.SuccessResult(result, "Stok record retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStokCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
                ApiResponse<object>.SuccessResult(result, "Stok record created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateStokCommand command)
        {
            var updatedCommand = new UpdateStokCommand(
                id,
                command.StokKodu,
                command.StokAdi,
                command.Birim,
                command.AlisFiyati,
                command.SatisFiyati,
                command.StokMiktari,
                command.Kategori,
                command.AktifMi
            );
            
            var result = await _mediator.Send(updatedCommand);
            return Ok(ApiResponse<object>.SuccessResult(result, "Stok record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteStokCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResult(result, "Stok record deleted successfully"));
        }
    }
}