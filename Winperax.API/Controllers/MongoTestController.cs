using Microsoft.AspNetCore.Mvc;
using Winperax.Api.Context;

namespace Winperax.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoTestController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public MongoTestController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                var db = _context.GetCollection<dynamic>("test-collection");
                return Ok(new { Success = true, Message = "MongoDB bağlantısı başarılı!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
