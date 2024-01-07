using Microsoft.EntityFrameworkCore;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using AuctionAngular.Dtos.Location;

namespace AuctionAngular.Services
{
    public class LocationService : ILocationService
    {
        private readonly AuctionDbContext _dbContext;
        /// <inheritdoc/>
        public LocationService(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ViewLocationDto> GetByIdLocationAsync(int id)
        {
            var location = await _dbContext
                .Locations
                .FirstOrDefaultAsync(u => u.Id == id);

            if (location is null)
            {
                throw new NotFoundException("Location not found.");
            }

            var resultDto = ViewLocationDtoConvert(location);

            return resultDto;
        }

        public async Task<IEnumerable<ViewLocationDto>> GetLocationsAsync()
        {
            var locations= await _dbContext
                .Locations
                .ToListAsync();

            if (locations is null)
            {
                throw new NotFoundException("Location not found.");
            }

            List<ViewLocationDto> result = new List<ViewLocationDto>();

            foreach(var loc in locations)
            {
                var resultDto = ViewLocationDtoConvert(loc);

                result.Add(resultDto);
            }

            return result;
        }
        public ViewLocationDto ViewLocationDtoConvert(Location location)
        {
            return new ViewLocationDto()
            {
                id = location.Id,
                Name = location.Name,
                Phone = location.Phone,
                Email = location.Email,
                City = location.City,
                Street = location.Street,
                PostalCode = location.PostalCode,
            };
        }
    }
}
