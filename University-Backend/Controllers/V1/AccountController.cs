using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using University_Backend.Helpers;
using University_Backend.Models.Data;
using University_Backend.Models.JWT;

namespace University_Backend.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettins;
        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettins = jwtSettings;
        }

        [HttpPost]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public string SaludarAdmin()
        {
            return "Hola, administrador";
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public string SaludarUsuario()
        {
            return "Hola, usuario";
        }
    }
}