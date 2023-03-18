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
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocation()
        {
            var result = await this.service.GetAll();
            return Ok(result);
        }
    }
}
