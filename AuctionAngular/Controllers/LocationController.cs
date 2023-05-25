using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService service;
        /// <summary>
        /// Location Controller
        /// </summary>
        public LocationController(ILocationService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get Location List
        /// </summary>
        /// <returns>Ok with location list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpGet("locationList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewLocationDto>>> LocationList()
        {
            var result = await this.service.GetAll();
            return Ok(result);
        }
    }
}
