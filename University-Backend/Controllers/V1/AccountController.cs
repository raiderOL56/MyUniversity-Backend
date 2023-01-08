using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using University_Backend.Helpers;
using University_Backend.Models.Data;
using University_Backend.Models.JWT;

namespace University_Backend.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettins;
        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettins = jwtSettings;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public IActionResult GetToken(AccountUser accountUser)
        {
            try
            {
                UserToken userToken = JwtHelper.GenerateToken(new UserToken()
                {
                    Username = accountUser.Username,
                    Id = 1,
                    GuidId = Guid.NewGuid()
                }, _jwtSettins);

                return Ok(userToken);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public string SaludarAdmin()
        {
            return "Hola, administrador";
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public string SaludarUsuario()
        {
            return "Hola, usuario";
        }
    }
}