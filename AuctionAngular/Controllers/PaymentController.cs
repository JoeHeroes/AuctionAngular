using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Payment Controller
        /// </summary>
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        /// <summary>
        /// Get Payments List
        /// </summary>
        /// <returns>Ok with payment list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ViewPaymentDto>>> GetAllPayments()
        {
            var result = await _paymentService.GetPaymentsAsync();

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        /// <summary>
        /// Get One Payment
        /// </summary>
        /// <returns>Ok with payments</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ViewPaymentDto>> GetOne([FromRoute] int id)
        {
            var result = await _paymentService.GetByIdPaymentAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        /// <summary>
        /// Delete Payment
        /// </summary>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePayment([FromRoute] int id)
        {
            await _paymentService.DeletePaymentAsync(id);

            return NoContent();
        }


        /// <summary>
        /// Create Payment
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
        {
            int id = await _paymentService.CreatePaymentAsync(dto);

            return Ok(id);
        }


        /// <summary>
        /// Update Payment
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="204">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpPatch("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePayment([FromRoute] int id, [FromBody] EditPaymentDto dto)
        {
            await _paymentService.UpdatePaymentAsync(id, dto);

            return NoContent();
        }

    }
}
