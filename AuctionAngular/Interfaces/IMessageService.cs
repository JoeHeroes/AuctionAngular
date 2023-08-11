using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IMessageService
    {
        Task CreateAsync(Message dto);
        bool CheckAsync(string token, string email);
    }
}
