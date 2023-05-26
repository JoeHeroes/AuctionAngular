using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface ICalendarService
    {
        Task<IEnumerable<ViewEventDto>> GetAll();
    }
}
