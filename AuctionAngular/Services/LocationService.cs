using AuctionAngular.Models.DTO;
using AuctionAngular.Models;

namespace AuctionAngular.Services
{

    public interface ILocationService
    {
        IEnumerable<Location> GetAll();
        Location GetById(int id);
    }
    public class LocationService : ILocationService
    {
        private readonly AuctionDbContext dbContext;
        public LocationService(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Location GetById(int id)
        {
            var location = this.dbContext
                .Locations
                .FirstOrDefault(u => u.Id == id);

            if (location is null)
            {
                throw new NotFoundException("Location not found");
            }

            return location;
        }

        public IEnumerable<Location> GetAll()
        {
            var location = this.dbContext
                .Locations
                .ToList();

            if (location is null)
            {
                throw new NotFoundException("Location not found");
            }

            return location;
        }

        
    }
}
