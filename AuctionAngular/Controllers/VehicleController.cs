using AuctionAngular.DTO;
using AuctionAngular.Enum;
using AuctionAngular.Models;
using AuctionAngular.Models.DTO;
using AuctionAngular.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService service;

        public VehicleController(IVehicleService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehicle()
        {
            try
            {
                var result = await this.service.GetAll();

                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetOne([FromRoute] int id)
        {
            try
            {
                var result = await this.service.GetById(id);

                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await this.service.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto dto)
        {

            try
            {
                await this.service.Create(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }

            return Ok();
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateVehicle([FromBody] EditVehicleDto dto)
        {
            try
            {
                await this.service.Update(dto);
               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

            return Ok();
        }


        [HttpPost("bid")]
        public async Task<IActionResult> UpdateBid([FromBody] UpdateBidDto dto)
        {


            try
            {
                await this.service.Bid(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

            return Ok();

        }

    }
}
