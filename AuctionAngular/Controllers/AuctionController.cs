using AuctionAngular.Models;
using AuctionAngular.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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

        [HttpGet("live")]
        public async Task<ActionResult<bool>> LiveAuction()
        {
            var result = await this.service.LiveAuction();
            return Ok(result);
        }

        [HttpGet("liveAuctionList")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> LiveAuctionList()
        {
            var result = await this.service.LiveAuctionList();
            return Ok(result);
        }
    }
}
