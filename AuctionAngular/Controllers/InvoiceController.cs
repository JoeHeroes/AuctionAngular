using AuctionAngular.Interfaces;
using AuctionAngular.Services.Invoice;
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
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GeneratePDF([FromBody] Info info)
        {
            var file = await _invoiceService.GeneratePDFAsync(info);

            return File(file.Response, "application/pdf", file.Filename);

        }
    }
}
