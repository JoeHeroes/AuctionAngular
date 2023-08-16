using AuctionAngular.Interfaces;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAngular.Controllers
{

    /// <summary>
    /// Invoice Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }


        /// <summary>
        /// Generate PDF
        /// </summary>
        /// <returns>Ok with payment list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GeneratePDF([FromQuery] string InvoiceNo)
        {
            var file = await _invoiceService.GeneratePDFAsync(InvoiceNo);

            return File(file.Response, "application/pdf", file.Filename);

        }

        /// <summary>
        /// Generate PDF Address
        /// </summary>
        /// <returns>Ok with payment list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GeneratePDFAddress()
        {
            await _invoiceService.GeneratePDFAddressAsync();

            return Ok();
        }

        /// <summary>
        /// Generate PDF With Image
        /// </summary>
        /// <returns>Ok with payment list</returns>
        /// <response code="200">Correct data</response>
        /// <response code="400">Incorrect data</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GeneratePDFWithImage()
        {
            await _invoiceService.GeneratePDFWithImageAsync();

            return Ok();
        }
    }
}
