using AuctionAngular.Dtos.MailDto;
using AuctionAngular.Interfaces;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly AuctionDbContext _dbContext;

        /// <summary>
        /// Mail Controller
        /// </summary>
        public MailController(IMailService mailService, AuctionDbContext dbContext)
        {
            _mailService = mailService;
            _dbContext = dbContext;
        }


        /// <summary>
        /// Send Email
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct send</response>
        /// <response code="400">Incorrect send</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SendEmail([FromBody] MailDto dto)
        {
            var from = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == dto.FromId);

            var to = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == dto.ToId);

            var mail = new MailRequestDto()
            {
                ToEmail = to.Email,
                Subject = dto.Title + "Emial Form " + from.Email,
                Body = dto.Body
            };

            await _mailService.SendEmailAsync(mail);
            return Ok();
        }
    }
}
