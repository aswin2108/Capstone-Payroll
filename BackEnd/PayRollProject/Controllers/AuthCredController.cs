using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using PayRollProject.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


namespace PayRollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthCredController : ControllerBase
    {
        private readonly AuthCredService _authCredService;

        public AuthCredController(AuthCredService authCredService)
        {
            _authCredService = authCredService;
        }

        [HttpPost("tryLogging")]
        public IActionResult GetUserDetails([FromBody] LoginData loginData)
        {

            string token=_authCredService.GetUserDetails(loginData.UserName, loginData.Password);
            if (token == null)
            {
                return Ok(new { message = "Unauthorised" }); // Create an anonymous object with the error message
            }

            return Ok(JsonSerializer.Serialize(token)); // Convert token to JSON string

        }

        [Authorize(Roles = "Admin, HR")]
        [HttpPost("createUser")]
        public IActionResult InsertUser([FromBody] NewAuth newUser )
        {
            string status=_authCredService.InsertUser(newUser.UserName, newUser.Password, newUser.Role);
            return Ok(status);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteAuth/{UserName}")]
        public IActionResult DeleteUserDetails(string UserName) { 
              _authCredService.DeleteUserDetails(UserName);
            return Ok();
        }


    }
}
