using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using AuctionAngular.Services;
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
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        /// <summary>
        /// Get Current Live Auction
        /// </summary>
        /// <returns>Ok with list of current live vehicle</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> LiveAuction()
        {
            var result = await _auctionService.LiveAuctionAsync();
            return Ok(result);
        }


        /// <summary>
        /// Start of the auction
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> StartAuction()
        {
            await _auctionService.StartAuctionAsync();
            return Ok();
        }



        /// <summary>
        /// End of the auction
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EndAuction()
        {
            await _auctionService.EndAuctionAsync();
            return NoContent();
        }


        /// <summary>
        /// Create Auction
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionDto dto)
        {
            await _auctionService.CreateAuctionAsync(dto);
            return Ok();
        }



        /// <summary>
        /// Delete Auction
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAuction([FromRoute] int id)
        {
            await _auctionService.DeleteAuctionAsync(id);
            return NoContent();
        }



        /// <summary>
        /// Edit Auction
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect id</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAuction([FromBody] EditAuctionDto dto)
        {
            await _auctionService.EditAuctionAsync(dto);
            return Ok();
        }

        /// <summary>
        /// Get One Auction
        /// </summary>
        /// <returns>Ok with auction</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ViewAuctionDto>> GetOne([FromRoute] int id)
        {
            var result = await _auctionService.GetByIdAuctionAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get Live Auction Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> LiveAuctionList()
        {
            var result = await _auctionService.LiveAuctionListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get Auction Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> AuctionList()
        {
            var result = await _auctionService.AuctionListAsync();
            return Ok(result);
        }
    }
}
