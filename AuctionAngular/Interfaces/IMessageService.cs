using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessageAsync(Message dto);
        Task<bool> CheckMessageAsync(string token, string email);
    }
}
