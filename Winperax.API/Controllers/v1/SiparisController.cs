using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.Siparis; // CreateSiparisCommand, UpdateSiparisCommand, DeleteSiparisCommand, GetAllSiparisQuery, GetSiparisByIdQuery için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class SiparisController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SiparisController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSiparisQuery());
            return Ok(
                ApiResponse<object>.Success(result, "Siparis records retrieved successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetSiparisByIdQuery(id));
            return Ok(ApiResponse<object>.Success(result, "Siparis record retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSiparisCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                ApiResponse<object>.Success(result, "Siparis record created successfully", 201)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSiparisCommand command)
        {
            // Command sınıfında Id property'sine atama yapamayacağımız için, yeni bir instance oluşturuyoruz
            var updatedCommand = new UpdateSiparisCommand(
                id,
                command.SiparisNo,
                command.CariId,
                command.SiparisTarihi,
                command.TeslimTarihi,
                command.Durum
            );

            var result = await _mediator.Send(updatedCommand);
            return Ok(ApiResponse<object>.Success(result, "Siparis record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteSiparisCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.Success(result, "Siparis record deleted successfully"));
        }
    }
}
