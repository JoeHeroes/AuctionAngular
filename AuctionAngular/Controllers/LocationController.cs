using AuctionAngular.Dtos.Location;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        /// <summary>
        /// Location Controller
        /// </summary>
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Get Location List
        /// </summary>
        /// <returns>Ok with location list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewLocationDto>>> LocationList()
        {
            var result = await _locationService.GetLocationsAsync();
            return Ok(result);
        }
    }
}
