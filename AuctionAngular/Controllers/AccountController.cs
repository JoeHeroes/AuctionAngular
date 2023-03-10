using AuctionAngular.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.DTO
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAccountServices service;

        public AccountController(IAccountServices service)
        {
            this.service = service;
        }


        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDto dto)
        {
            this.service.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token =  this.service.GeneratJwt(dto);

            return Ok(token);
        }


        [HttpPost("restart")]
        public ActionResult Restart([FromBody] RestartPasswordDto dto)
        {
            this.service.RestartPassword(dto);

            return Ok();
        }

    }
}
