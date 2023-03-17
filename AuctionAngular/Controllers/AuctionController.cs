using AuctionAngular.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly IVehicleService service;

        public AuctionController(IVehicleService service)
        {
            this.service = service;
        }

    }
}
