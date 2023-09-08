using AuctionAngular.Dtos;
using Database.Entities;

namespace AuctionAngular.Interfaces
{
    public interface ICalendarService
    {
        Task<IEnumerable<ViewEventDto>> GetEventsAsync(int userId);
        Task<ViewEventDto> GetByIdEventAsync(int id);
        Task<int> CreateEventAsync(CreateEventDto dto);
        Task<Event> EditEventAsync(EditEventDto dto);
        Task<int> DeleteEventAsync(int id);
    }
}
