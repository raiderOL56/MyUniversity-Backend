using Microsoft.AspNetCore.Mvc;

namespace University_Backend.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ExampleController : ControllerBase
    {
        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult GetSaludo()
        {
            return Ok("Hola, soy V1");
        }
    }
}