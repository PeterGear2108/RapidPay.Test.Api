using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Test.Api.Services.Interfaces;
using RapidPay.Test.Models.Dto;

namespace RapidPay.Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
    
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] UserLoginDto user)
        {
            var userToken = _authService.Login(user.Username, user.Password);
            return Ok(userToken);
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create-user")]
        public IActionResult CreateUser([FromBody] UserLoginDto user)
        {
            _authService.CreateUser(user.Username, user.Password);
            return Ok();
        }


    }
}
