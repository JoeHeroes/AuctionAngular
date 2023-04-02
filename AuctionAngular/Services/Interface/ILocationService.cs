using AuctionAngular.Models;

namespace AuctionAngular.Services.Interface
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location> GetById(int id);
    }
}
