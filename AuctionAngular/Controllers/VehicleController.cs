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

        public VehicleController(IVehicleService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewVehiclesDto>>> GetAllVehicle()
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
        public async Task<ActionResult<ViewVehicleDto>> GetOne([FromRoute] int id)
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
            int id;

            try
            {
                id = await this.service.Create(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

            return Ok(id);
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


        [HttpPost("watch")]
        public async Task<IActionResult> Watch([FromBody] WatchDto dto)
        {
            try
            {
                await this.service.Watch(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

            return Ok();

        }


        [HttpPost("removeWatch")]
        public async Task<IActionResult> RemoveWatch([FromBody] WatchDto dto)
        {
            try
            {
                await this.service.RemoveWatch(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

            return Ok();

        }

        [HttpPost("checkWatch")]
        public async Task<ActionResult<bool>> CheckWatch([FromBody] WatchDto dto)
        {
            try
            {
                var result = await this.service.CheckWatch(dto);

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }
        
        [HttpGet("allWatch/{id}")]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> AllWatch([FromRoute] int id)
        {
            try
            {
                var result = await this.service.GetAllWatch(id);

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


        [HttpPost]
        [Route("uploadFile/{id}")]
        public async Task<IActionResult> UploadFile([FromRoute] int id)
        {
            IFormFileCollection files = Request.Form.Files;
            try
            {
                var result = await this.service.AddPicture(id, files);

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
    }
}
