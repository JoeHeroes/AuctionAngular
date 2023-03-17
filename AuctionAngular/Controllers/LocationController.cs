using AuctionAngular.Models;
using AuctionAngular.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService service;

        public LocationController(ILocationService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetAllLocation()
        {
            var location = this.service.GetAll();
            return Ok(location);
        }
    }
}
