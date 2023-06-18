using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface ICalendarService
    {
        Task<IEnumerable<ViewEventDto>> GetAll();
        Task<ViewEventDto> GetById(int id);
        Task<int> Create(CreateEventDto dto);
        Task Edit(EditEventDto dto);
    }
}
