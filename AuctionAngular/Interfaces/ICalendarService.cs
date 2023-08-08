using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface ICalendarService
    {
        Task<IEnumerable<ViewEventDto>> GetEventsAsync();
        Task<ViewEventDto> GetByIdEventAsync(int id);
        Task<int> CreateEventAsync(CreateEventDto dto);
        Task EditEventsAsync(EditEventDto dto);
    }
}
