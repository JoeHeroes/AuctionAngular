using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService service;
        /// <summary>
        /// Calendar Controller
        /// </summary>
        public CalendarController(ICalendarService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get Event List
        /// </summary>
        /// <returns>Ok with event list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpGet("eventList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Event>>> EventList()
        {
            var result = await this.service.GetAll();
            return Ok(result);
        }



        /// <summary>
        /// Create Event
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpPost("CreateEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEvent([FromBody]CreateEventDto dto)
        {
            var result = await this.service.Create(dto);
            return Ok();
        }
    }
}
