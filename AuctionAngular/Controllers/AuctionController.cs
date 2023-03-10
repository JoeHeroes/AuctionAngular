using AuctionAngular.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly IVehicleServices service;

        public AuctionController(IVehicleServices service)
        {
            this.service = service;
        }

    }
}
