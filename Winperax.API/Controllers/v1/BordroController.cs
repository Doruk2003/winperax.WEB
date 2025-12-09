using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winperax.API.Responses;
using Winperax.Application.Modules.Bordro; // CreateBordroCommand, UpdateBordroCommand, DeleteBordroCommand, GetAllBordroQuery, GetBordroByIdQuery için

namespace Winperax.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulama zorunlu
    public class BordroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BordroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllBordroQuery());
            return Ok(ApiResponse<object>.SuccessResult(result, "Bordro records retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetBordroByIdQuery(id));
            return Ok(ApiResponse<object>.SuccessResult(result, "Bordro record retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBordroCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
                ApiResponse<object>.SuccessResult(result, "Bordro record created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateBordroCommand command)
        {
            var updatedCommand = new UpdateBordroCommand(
                id,
                command.PersonelId,
                command.Donem,
                command.BrutMaas,
                command.NetMaas,
                command.EkOdeme,
                command.Kesinti,
                command.ToplamOdenecek,
                command.OdemeTarihi
            );
            
            var result = await _mediator.Send(updatedCommand);
            return Ok(ApiResponse<object>.SuccessResult(result, "Bordro record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteBordroCommand(id);
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.SuccessResult(result, "Bordro record deleted successfully"));
        }
    }
}