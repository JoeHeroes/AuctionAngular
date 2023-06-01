using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService service;

        /// <summary>
        /// Vehicle Controller
        /// </summary>
        public VehicleController(IVehicleService service)
        {
            this.service = service;
        }
        /// <summary>
        /// Get Vehicle List
        /// </summary>
        /// <returns>Ok with vehicle list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllVehicle()
        {
            var result = await this.service.GetAll();

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
        [HttpGet("getOne/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<ViewVehicleDto>> GetOne([FromRoute] int id)
        {
            var result = await this.service.GetById(id);

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
        [HttpGet("getAllBided/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllBidedVehicle([FromRoute] int id)
        {
            var result = await this.service.GetAllBided(id);

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
        [HttpGet("getAllWon/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllWonVehicle([FromRoute] int id)
        {
            var result = await this.service.GetAllWon(id);

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
        [HttpGet("getAllLost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllLostVehicle([FromRoute] int id)
        {
            var result = await this.service.GetAllLost(id);

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
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await this.service.Delete(id);

            return NoContent();
        }


        /// <summary>
        /// Create Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto dto)
        {
            int id = await this.service.Create(dto);

            return Ok(id);
        }


        /// <summary>
        /// Update Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="204">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPatch("update/{id}")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVehicle([FromRoute] int id,[FromBody] EditVehicleDto dto)
        {
            await this.service.Update(id, dto);

            return NoContent();
        }


        /// <summary>
        /// Bid Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPatch("bid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateBid([FromBody] UpdateBidDto dto)
        {
            await this.service.Bid(dto);

            return Ok();
        }

        /// <summary>
        /// Watch Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("watch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Watch([FromBody] WatchDto dto)
        {
            await this.service.Watch(dto);

            return Ok();

        }

        /// <summary>
        /// Remove Watch Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("removeWatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveWatch([FromBody] WatchDto dto)
        {
            await this.service.RemoveWatch(dto);

            return Ok();
        }

        /// <summary>
        /// Check Watch Vehicle
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("checkWatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CheckWatch([FromBody] WatchDto dto)
        {
            var result = await this.service.CheckWatch(dto);

            return Ok(result);
        }



        /// <summary>
        /// Get Watch Vehicle List
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("allWatch/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> AllWatch([FromRoute] int id)
        {
            var result = await this.service.GetAllWatch(id);

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
        [HttpPatch("uploadFile/{id}")]

        public async Task<IActionResult> UploadFile([FromRoute] int id)
        {
            IFormFileCollection files = Request.Form.Files;

            var result = await this.service.AddPicture(id, files);

            if (result is null)
            {
                return NotFound();
            }

            var response = new { Message = "Upload successfully!" };
            return Ok(response);
        }
    }
}
