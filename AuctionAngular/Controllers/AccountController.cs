using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionAngular.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="dto">Data needed for registration</param>
        /// <returns>Ok</returns>
        /// <response code="201">Correct register new user</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await this.service.RegisterUser(dto);
            return Ok();
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="dto">Data needed for login</param>
        /// <returns>Ok with AuthResponseDto</returns>
        /// <response code="200">Correct login</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await this.service.GeneratJwt(dto);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        /// <summary>
        /// Restart User Password
        /// </summary>
        /// <param name="dto">Data needed for restart</param>
        /// <returns>Ok with AuthResponseDto</returns>
        /// <response code="200">Correct restart</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("restart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Restart([FromBody] RestartPasswordDto dto)
        {
            await this.service.RestartPassword(dto);

            return Ok();
        }

        /// <summary>
        /// Edit User Data
        /// </summary>
        /// <param name="dto">Data needed for edit</param>
        /// <returns>Ok</returns>
        /// <response code="204">Correct edit</response>
        /// <response code="400">Incorrect data</response>
        [HttpPatch("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromBody] EditUserDto dto)
        {
            await this.service.EditProfile(dto);

            return NoContent();
        }


        /// <summary>
        /// Current Logged User
        /// </summary>
        /// <returns>Ok with id</returns>
        /// <response code="200">Correct id</response>
        /// <response code="400">Null id </response>
        [Authorize]
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CurrentLoggedUserId()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            return Ok(new { userId = id });
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok user information</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpGet("userInfo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserInfo([FromRoute] int id)
        {
            var result = await this.service.GetUserInfo(id);

            return Ok(result);
        }



        /// <summary>
        /// Get Roles
        /// </summary>
        /// <returns>Ok with list of roles</returns>
        /// <response code="200">Correct data</response>
        [HttpGet("roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoles()
        {
            var result = await this.service.GetRole();

            return Ok(result);
        }

        /// <summary>
        /// Upload Picture File
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok with messages or StatusCode</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        /// <response code="500">Exception</response>
        [HttpPatch("uploadFile/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UploadFile([FromRoute] int id)
        {
            IFormFile files = Request.Form.Files[0];
            try
            {
                var result = await this.service.AddPicture(id, files);

                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    var response = new { Message = "Upload successfully!" };
                    return Ok(response);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }
    }
}
