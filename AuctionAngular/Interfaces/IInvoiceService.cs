using AuctionAngular.Dtos;
using AuctionAngular.Dtos.Invoice;

namespace AuctionAngular.Interfaces
{
    public interface IInvoiceService
    {
        Task<PDFResponseDto> GeneratePDFAsync(InfoDto info);
        Task<IEnumerable<ViewInvoicesDto>> GetInvoicesAsync();
        string Getbase64string();
    }
}