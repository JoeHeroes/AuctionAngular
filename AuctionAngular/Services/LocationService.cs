using AuctionAngular.Models.DTO;
using AuctionAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{

    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location> GetById(int id);
    }
    public class LocationService : ILocationService
    {
        private readonly AuctionDbContext dbContext;
        public LocationService(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Location> GetById(int id)
        {
            var result = await this.dbContext
                .Locations
                .FirstOrDefaultAsync(u => u.Id == id);

            if (result is null)
            {
                throw new NotFoundException("Location not found");
            }

            return result;
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            var result = this.dbContext
                .Locations
                .ToList();

            if (result is null)
            {
                throw new NotFoundException("Location not found");
            }

            return result;
        }

        
    }
}
