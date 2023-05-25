using Microsoft.EntityFrameworkCore;
using AuctionAngular.Interfaces;
using AuctionAngular.Dtos;
using Database;

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
            var result = await this.dbContext
                .Locations
                .FirstOrDefaultAsync(u => u.Id == id);

            if (result is null)
            {
                throw new NotFoundException("Location not found");
            }

            var resultDto = new ViewLocationDto()
            {
                Name = result.Name,
                Phone = result.Phone,
                Email = result.Email,
                City = result.City,
                Street = result.Street,
                PostalCode = result.PostalCode,
                Picture = result.Picture,
            };


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

                var resultDto = new ViewLocationDto()
                {
                    id= loc.Id,
                    Name = loc.Name,
                    Phone = loc.Phone,
                    Email = loc.Email,
                    City = loc.City,
                    Street = loc.Street,
                    PostalCode = loc.PostalCode,
                    Picture = loc.Picture,
                };

                result.Add(resultDto);
            }

            return result;
        }

        
    }
}
