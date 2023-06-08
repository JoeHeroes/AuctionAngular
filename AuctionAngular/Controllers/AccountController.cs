using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
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
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await this.service.RegisterUser(dto);


            var token = await _userManager.GenerateEmailConfirmTokenAsync(user);
            var confirmLink = Url.Action(nameof(ConfirmEmail), "Authication", new { token, email = User.Email });

            var message = new Message(new string[] { dto.Email }, "ConfirmEmail email link", confirmLink);


            var result = SendEmail(dto, confirmLink);

            if (result)
            {
                return Ok("Please verify your email");
            }

            return Ok("Please request an email verification link");
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


        /// <summary>
        /// Upload Picture File
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok with messages or StatusCode</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        /// <response code="500">Exception</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmail(int userId, string code)
        {
            if(userId == 0 || code == null)
            {
                return BadRequest();
            }

            var user = await this.service.GetUserInfo(userId);

            if(user == null)
            {
                return BadRequest();
            }

            code = Encoding.UTF8.GetString(Convert.FromBase64String(code));
            //var result = await this.service.ConfirmEmail(user, code);
            //var status = result.SucessStatus;

            return Ok();
        }


        private bool SendEmail(RegisterUserDto dto, string confirmLink)
        {
            var options = new RestClientOptions("https://api.mailgun.net/v3");
            options.Authenticator = new HttpBasicAuthenticator("api", "41df57fa62356e2c5fb2c0462b3e9abd-6d1c649a-92bb86b1");
            var client = new RestClient(options);
            
            var request = new RestRequest("", Method.Post);
            request.AddParameter("domain", "sandbox0e1fe82cedc54d2f915435caa729fae4.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "CarAuction <postmaster@sandbox0e1fe82cedc54d2f915435caa729fae4.mailgun.org>");
            request.AddParameter("to", $"{dto.Name} {dto.SureName} <{dto.Email}>");
            request.AddParameter("subject", $"Hello {dto.Name} {dto.SureName}, please verify your  Auction account");
            request.AddParameter("text", "Thank you for choosing CarAuction! Please confirm your email address by clicking the link below.");
            var response =  client.Execute(request);

            return response.IsSuccessful;

        }
        
    }
}
