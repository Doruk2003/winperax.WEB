using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.Cari; // CreateCariCommand, UpdateCariCommand, DeleteCariCommand, GetAllCariQuery, GetCariByIdQuery için

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
            return Ok(ApiResponse<object>.Success(result, "Cari records retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetCariByIdQuery(id));
            return Ok(ApiResponse<object>.Success(result, "Cari record retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCariCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                ApiResponse<object>.Success(result, "Cari record created successfully", 201)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCariCommand command)
        {
            // Command sınıfında Id property'sine atama yapamayacağımız için, yeni bir instance oluşturuyoruz
            var updatedCommand = new UpdateCariCommand(
                id,
                command.CariKodu,
                command.Unvan,
                command.VergiNo
            );

            var result = await _mediator.Send(updatedCommand);
            return Ok(ApiResponse<object>.Success(result, "Cari record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteCariCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.Success(result, "Cari record deleted successfully"));
        }
    }
}
