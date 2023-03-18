using AuctionAngular.Models;
using AuctionAngular.Models.DTO;
using AuctionAngular.Services;
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
            var result = await this.service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetOne([FromRoute] int id)
        {
            var result = await this.service.GetById(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await this.service.Delete(id);

            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleDto dto)
        {

            await this.service.Create(dto);

            return Ok();
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateVehicle([FromBody] EditVehicleDto dto)
        {

            await this.service.Update(dto);

            return Ok();
        }

    }
}
