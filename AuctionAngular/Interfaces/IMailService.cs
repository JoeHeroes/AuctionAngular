using AuctionAngular.Dtos.MailDto;

namespace AuctionAngular.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(MailRequestDto dto);
    }
}
