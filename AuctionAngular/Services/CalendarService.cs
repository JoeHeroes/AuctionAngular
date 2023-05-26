using Microsoft.EntityFrameworkCore;
using AuctionAngular.Interfaces;
using AuctionAngular.Dtos;
using Database;
using Database.Entities;
using System.Drawing;

namespace AuctionAngular.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly AuctionDbContext dbContext;
        /// <inheritdoc/>
        public CalendarService(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ViewEventDto>> GetAll()
        {
            var events = await this.dbContext
                .Events
                .ToListAsync();

            if (events is null)
            {
                throw new NotFoundException("Events not found");
            }

            List<ViewEventDto> result = new List<ViewEventDto>();

            foreach(var eve in events)
            {

                var resultDto = new ViewEventDto()
                {
                    Title = eve.Title,
                    Start = eve.Start.ToString("yyyy-MM-dd"),
                    End = eve.End.ToString("yyyy-MM-dd"),
                    Color = eve.Color,
                };

                result.Add(resultDto);
            }

            return result;
        }
    }
}
