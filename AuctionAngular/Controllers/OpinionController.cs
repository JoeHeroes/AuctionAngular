using AuctionAngular.Dtos.Opinion;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionService _opinionService;

        /// <summary>
        /// Opinion Controller
        /// </summary>
        public OpinionController(IOpinionService opinionService)
        {
            _opinionService = opinionService;
        }

        /// <summary>
        /// Create Opinion
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOpinion([FromBody] CreateOpinionDto dto)
        {
            int id = await _opinionService.CreateOpinionAsync(dto);

            return Ok(id);
        }


        /// <summary>
        /// Get One Opinion
        /// </summary>
        /// <returns>Ok with opinion</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ViewOpinionDto>> GetOne([FromRoute] int id)
        {
            var result = await _opinionService.GetByVehicleIdOpinionAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
