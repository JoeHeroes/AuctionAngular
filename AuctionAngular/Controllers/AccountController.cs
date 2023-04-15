using AuctionAngular.Models;
using AuctionAngular.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            await this.service.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await this.service.GeneratJwt(dto);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }


        [HttpPost("restart")]
        public async Task<IActionResult> Restart([FromBody] RestartPasswordDto dto)
        {
            await this.service.RestartPassword(dto);

            return Ok();
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> UserId([FromRoute] int id)
        {
            return Ok( new { userId = id});
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> CurrentLoggedUserId()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            return Ok(new { userId = id });
        }

    }
}
