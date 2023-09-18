using AuctionAngular.Dtos;
using AuctionAngular.Dtos.User;
using AuctionAngular.Interfaces;
using Database.Entities;
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
        private readonly IAccountService _accountService;
        private readonly IMailService _mailService;
        private readonly IMessageService _messageService;

        public AccountController(IAccountService accountService, IMailService mailService, IMessageService messageService)
        {
            _accountService = accountService;
            _mailService = mailService;
            _messageService = messageService;
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
            await _accountService.CreateUserAsync(dto);

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

            var token = await _accountService.GenerateTokenAsync(user);

            await SendEmail(dto.Email);

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
            var token = await _accountService.LoginUserAsync(dto);

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
            await _accountService.RestartPasswordAsync(dto);

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
            await _accountService.EditProfileAsync(dto);

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
            var result = await _accountService.GetUserInfoAsync(id);

            return Ok(result);
        }


        /// <summary>
        /// Get User Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok user information</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserRole([FromRoute] int id)
        {
            var result = await _accountService.GetUserRoleAsync(id);

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
            var result = await _accountService.GetAllRoleAsync();

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

            var result = await _accountService.AddProfilePictureAsync(id, files);

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
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _accountService.GetByEmailAsync(email);

            var token = await _accountService.GenerateTokenAsync(user);

            string link = "https://localhost:7257" + "/Account/ConfirmEmail/"+ token + "/" + email;

            string LOGIN_EMAIL_CONTENT_FORMAT = "<h1>Account Verification</h1></br><p>Thank you for choosing CarAuction</p></br> <p>Please confirm your email address by clicking the link below. </br> <a style=\"color: blue\"  href=\"{Link}\">Verify your email address</a></b></p>";
            string content = LOGIN_EMAIL_CONTENT_FORMAT.Replace("{Link}", link);

            var mail = new MailRequestDto()
            {
                ToEmail = "JoeHeros@wp.pl",
                Subject = $"Hi {user.Name} {user.SureName}, please verify your CarAuction account",
                Body = content
            };
            var result = await _mailService.SendEmailAsync(mail);


            Message authenticationMessage = new Message()
            {
                Email = email,
                Sent = result,
                Title = "Logowanie do Panelu klienta",
                Content = content,
                Data = token,
                Date = DateTime.Now,
            };
            await _messageService.CreateMessageAsync(authenticationMessage);

            if (result)
            {
                return Ok("Please verify your email");
            }

            return Problem();
        }


        [HttpGet("[action]/{email}")]

        public async Task<IActionResult> CheckEmail([FromRoute] string email)
        {
            var result = await _accountService.GetByEmailAsync(email);

            if(result is null)
            {
                return NotFound();
            }
            return Ok(result.EmialConfirmed);
        }



        [HttpGet("[action]/{token}/{email}")]

        public async Task<IActionResult> ConfirmEmail([FromRoute] string token, [FromRoute] string email)
        {
            bool resultCheck = await _messageService.CheckMessageAsync(token, email);

            User user = await _accountService.GetByEmailAsync(email);

            await _accountService.AccountVerification(user.Id ,resultCheck);

            return Ok(token);
        }
    }
}
