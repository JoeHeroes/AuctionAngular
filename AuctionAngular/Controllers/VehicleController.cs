using AuctionAngular.Dtos.Bid;
using AuctionAngular.Dtos.Vehicle;
using AuctionAngular.Dtos.Watch;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        /// <summary>
        /// Vehicle Controller
        /// </summary>
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        /// <summary>
        /// Get Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllVehicle()
        {
            var result = await _vehicleService.GetVehiclesAsync();

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        /// <summary>
        /// Get Vehicle List which auction end 
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewAdminVehiclesDto>>> GetAllVehicleAuctionEnd()
        {
            var result = await _vehicleService.GetVehicleAuctionEndAsync();

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        



        /// <summary>
        /// Get One Vehicle
        /// </summary>
        /// <returns>Ok with vehicle</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<ViewVehicleDto>> GetOne([FromRoute] int id)
        {
            var result = await _vehicleService.GetByIdVehicleAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        /// <summary>
        /// Get Bided Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllBidedVehicle([FromRoute] int id)
        {
            var result = await _vehicleService.GetAllBidedAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }




        /// <summary>
        /// Get Won Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllWonVehicle([FromRoute] int id)
        {
            var result = await _vehicleService.GetAllWonAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get Lost Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllLostVehicle([FromRoute] int id)
        {
            var result = await _vehicleService.GetAllLostAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        /// <summary>
        /// Delete Vehicle
        /// </summary>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);

            return NoContent();
        }


        /// <summary>
        /// Create Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto dto)
        {
            int id = await _vehicleService.CreateVehicleAsync(dto);

            return Ok(id);
        }
        

        /// <summary>
        /// Update Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="204">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPatch("[action]/{id}")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVehicle([FromRoute] int id,[FromBody] EditVehicleDto dto)
        {
            await _vehicleService.UpdateVehicleAsync(id, dto);

            return NoContent();
        }
        

        /// <summary>
        /// Bid Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPatch("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateBid([FromBody] UpdateBidDto dto)
        {

            if(await _vehicleService.BidVehicleAsync(dto))
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Watch Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Watch([FromBody] WatchDto dto)
        {
            await _vehicleService.WatchVehicleAsync(dto);

            return Ok();
        }

        /// <summary>
        /// Remove Watch Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveWatch([FromBody] WatchDto dto)
        {
            await _vehicleService.RemoveWatchAsync(dto);

            return Ok();
        }

        /// <summary>
        /// Check Watch Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CheckWatch([FromBody] WatchDto dto)
        {
            var result = await _vehicleService.CheckWatchAsync(dto);

            return Ok(result);
        }



        /// <summary>
        /// Get Watch Vehicle List
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> AllWatch([FromRoute] int id)
        {
            var result = await _vehicleService.GetAllWatchAsync(id);

            if (result is null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> UploadVehicleImage([FromRoute] int id)
        {
            IFormFileCollection files = Request.Form.Files;

            var result = await _vehicleService.AddPictureAsync(id, files);

            if (result is null)
            {
                return NotFound();
            }

            var response = new { Message = "Upload successfully!" };
            return Ok(response);
        }


        /// <summary>
        /// Sold Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> SoldVehicle([FromRoute] int id)
        {
            await _vehicleService.SoldVehicleAsync(id);

            return Ok();
        }
    }
}
