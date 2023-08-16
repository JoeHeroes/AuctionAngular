using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface IInvoiceService
    {
        Task<PDFResponseDto> GeneratePDFAsync(string InvoiceNo);
        Task GeneratePDFAddressAsync();
        Task GeneratePDFWithImageAsync();
        string Getbase64string();
    }
}
