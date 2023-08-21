using AuctionAngular.Dtos;
using AuctionAngular.Services.NewFolder;

namespace AuctionAngular.Interfaces
{
    public interface IInvoiceService
    {
        Task<PDFResponseDto> GeneratePDFAsync(PDFInfo info);
        string Getbase64string();
    }
}
