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
            return Ok(ApiResponse<object>.Success(result, "Finans records retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetFinansByIdQuery(id));
            return Ok(ApiResponse<object>.Success(result, "Finans record retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFinansCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                ApiResponse<object>.Success(result, "Finans record created successfully", 201)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateFinansCommand command)
        {
            // Command sınıfında Id property'sine atama yapamayacağımız için, yeni bir instance oluşturuyoruz
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
            return Ok(ApiResponse<object>.Success(result, "Finans record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteFinansCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.Success(result, "Finans record deleted successfully"));
        }
    }
}
