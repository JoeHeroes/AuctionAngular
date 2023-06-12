using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(MailRequestDto dto);
    }
}
