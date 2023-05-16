using AuctionAngular.Entities;

namespace AuctionAngular.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location> GetById(int id);
    }
}
