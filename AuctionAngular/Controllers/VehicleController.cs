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
        private readonly IVehicleServices service;

        public VehicleController(IVehicleServices service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetAllVehicle()
        {
            var vehicle = this.service.GetAll();
            return Ok(vehicle);
        }

        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetOne([FromRoute] int id)
        {
            var vehicle = this.service.GetById(id);

            return Ok(vehicle);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.service.Delete(id);

            return NotFound();
        }

        [HttpPost("create")]
        public ActionResult CreateVehicle([FromBody] VehicleDto dto)
        {

            this.service.Create(dto);

            return Ok();
        }


        [HttpPost("update")]
        public ActionResult UpdateVehicle([FromBody] EditVehicleDto dto)
        {

            this.service.Update(dto);

            return Ok();
        }

    }
}
