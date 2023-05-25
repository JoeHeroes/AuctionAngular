using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    /// <summary>
    /// Auction Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService service;

        public AuctionController(IAuctionService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get Current Live Auction
        /// </summary>
        /// <returns>Ok with list of current live vehicle</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpGet("liveAuction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> LiveAuction()
        {
            var result = await this.service.LiveAuction();
            return Ok(result);
        }


        /// <summary>
        /// End of the auction
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("endAuction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EndAuction()
        {
            await this.service.EndAuction();
            return NoContent();
        }

        /// <summary>
        /// Get Live Auction Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("liveAuctionList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> LiveAuctionList()
        {
            var result = await this.service.LiveAuctionList();
            return Ok(result);
        }

        /// <summary>
        /// Get Auction Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("auctionList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> AuctionList()
        {
            var result = await this.service.AuctionList();
            return Ok(result);
        }

    }
}
