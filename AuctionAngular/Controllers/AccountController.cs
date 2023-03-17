using AuctionAngular.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.DTO
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            this.service.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            string token =  this.service.GeneratJwt(dto);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }


        [HttpPost("restart")]
        public ActionResult Restart([FromBody] RestartPasswordDto dto)
        {
            this.service.RestartPassword(dto);

            return Ok();
        }

    }
}
