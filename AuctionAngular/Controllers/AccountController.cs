using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

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
        private readonly IMailService mailService;

        public AccountController(IAccountService service, IMailService mailService)
        {
            this.service = service;
            this.mailService = mailService;
        }
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="dto">Data needed for registration</param>
        /// <returns>Ok</returns>
        /// <response code="201">Correct register new user</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await this.service.CreateUser(dto);

            var user = new User()
            {
                Email = dto.Email,
                Name = dto.Name,
                SureName = dto.SureName,
                PasswordHash = dto.Password,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                Phone = dto.Phone,
                RoleId = dto.RoleId,
                ProfilePicture = "",
                EmialConfirmed = false
            };

            var token = await this.service.GenerateToken(user);

            return Ok(token);
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="dto">Data needed for login</param>
        /// <returns>Ok with AuthResponseDto</returns>
        /// <response code="200">Correct login</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var token = await this.service.LoginUser(dto);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        /// <summary>
        /// Restart User Password
        /// </summary>
        /// <param name="dto">Data needed for restart</param>
        /// <returns>Ok with AuthResponseDto</returns>
        /// <response code="200">Correct restart</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
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
        [HttpPatch("[action]")]
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
        [HttpGet("[action]")]
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
        [HttpGet("[action]/{id}")]
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
        [HttpGet("[action]")]
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
        [HttpPatch("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UploadProfileImage([FromRoute] int id)
        {
            IFormFile files = Request.Form.Files[0];

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


        [HttpGet("[action]")]
        public async Task<IActionResult> Account_Info()
        {

            var user = new User()
            {
                Email = "JoeHeros@wp.pl",
                Name = "string",
                SureName = "string",
                PasswordHash = "string",
                DateOfBirth = new DateTime(),
                Nationality = "string",
                Phone = "string",
                RoleId = 0,
                ProfilePicture = "",
                EmialConfirmed = false
            };


            var token = await this.service.GenerateToken(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = encodedToken }, Request.Scheme);

            var mail = new MailRequestDto()
            {
                ToEmail="JoeHeros@wp.pl",
                Subject="lol",
                Body = callbackUrl
            };
            var result = await this.mailService.SendEmailAsync(mail);


            if (result)
            {
                return Ok("Please verify your email");
            }

            return Problem();
        }
    }
}
