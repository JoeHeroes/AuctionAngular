﻿using AuctionAngular.Dtos.Event;
using AuctionAngular.Interfaces;
using AuctionAngular.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;
        /// <summary>
        /// Calendar Controller
        /// </summary>
        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        /// <summary>
        /// Get Event List
        /// </summary>
        /// <returns>Ok with event list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewEventDto>>> EventList([FromRoute] int userId)
        {
            var result = await _calendarService.GetEventsAsync(userId);
            return Ok(result);
        }


        /// <summary>
        /// Get One Event
        /// </summary>
        /// <returns>Ok with event</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ViewEventDto>> GetOne([FromRoute] int id)
        {
            var result = await _calendarService.GetByIdEventAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Create Event
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEvent([FromBody]CreateEventDto dto)
        {
            var result = await _calendarService.CreateEventAsync(dto);
            return Ok();
        }

        /// <summary>
        /// Edit Event
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditEvent([FromBody] EditEventDto dto)
        {
            await _calendarService.EditEventAsync(dto);
            return Ok();
        }



        /// <summary>
        /// Delete Event
        /// </summary>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            await _calendarService.DeleteEventAsync(id);

            return NoContent();
        }
    }
}
