using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface IMessageService
    {
        Task Create(Message dto);
        bool Check(string token, string email);
    }
}
