using AuctionAngular.Dtos;
using AuctionAngular.Services.Invoice;

namespace AuctionAngular.Interfaces
{
    public interface IInvoiceService
    {
        Task<PDFResponseDto> GeneratePDFAsync(Info info);
        string Getbase64string();
    }
}
