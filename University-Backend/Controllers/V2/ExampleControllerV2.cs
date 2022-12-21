using Microsoft.AspNetCore.Mvc;

namespace University_Backend.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ExampleController : ControllerBase
    {
        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetSaludo()
        {
            return Ok("Hola, soy V2");
        }
    }
}