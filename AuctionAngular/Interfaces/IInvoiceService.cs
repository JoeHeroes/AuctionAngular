using AuctionAngular.Dtos;
using AuctionAngular.Dtos.Invoice;

namespace AuctionAngular.Interfaces
{
    public interface IInvoiceService
    {
        Task<PDFResponseDto> GeneratePDFAsync(InfoDto info);
        string Getbase64string();
    }
}