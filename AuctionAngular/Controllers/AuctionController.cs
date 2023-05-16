using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService service;

        public AuctionController(IAuctionService service)
        {
            this.service = service;
        }

        [HttpGet("liveAuction")]
        public async Task<ActionResult<bool>> LiveAuction()
        {
            var result = await this.service.LiveAuction();
            return Ok(result);
        }

        [HttpGet("endAuction")]
        public async Task<ActionResult> EndAuction()
        {
            await this.service.EndAuction();
            return Ok();
        }

        [HttpGet("liveAuctionList")]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> LiveAuctionList()
        {
            var result = await this.service.LiveAuctionList();
            return Ok(result);
        }

        [HttpGet("auctionList")]
        public async Task<ActionResult<IEnumerable<ViewVehicleDto>>> AuctionList()
        {
            var result = await this.service.AuctionList();
            return Ok(result);
        }

    }
}
