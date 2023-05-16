using AuctionAngular.Dtos;

namespace AuctionAngular.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<ViewLocationDto>> GetAll();
        Task<ViewLocationDto> GetById(int id);
    }
}
