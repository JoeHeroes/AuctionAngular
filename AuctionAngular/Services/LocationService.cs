using Microsoft.EntityFrameworkCore;
using AuctionAngular.Interfaces;
using AuctionAngular.Dtos;
using Database;
using Database.Entities;
using NLog.Fluent;

namespace AuctionAngular.Services
{
    public class LocationService : ILocationService
    {
        private readonly AuctionDbContext dbContext;
        /// <inheritdoc/>
        public LocationService(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ViewLocationDto> GetById(int id)
        {
            var location = await this.dbContext
                .Locations
                .FirstOrDefaultAsync(u => u.Id == id);

            if (location is null)
            {
                throw new NotFoundException("Location not found");
            }

            var resultDto = ViewLocationDtoConvert(location);

            return resultDto;
        }

        public async Task<IEnumerable<ViewLocationDto>> GetAll()
        {
            var locations= await this.dbContext
                .Locations
                .ToListAsync();

            if (locations is null)
            {
                throw new NotFoundException("Location not found");
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
                Picture = location.Picture,
            };
        }
    }
}
