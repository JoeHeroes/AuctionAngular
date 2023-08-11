using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<ViewLocationDto>> GetLocationsAsync();
        Task<ViewLocationDto> GetByIdLocationAsync(int id);
    }
}
