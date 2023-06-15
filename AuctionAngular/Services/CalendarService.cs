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
                    Id = eve.Id,
                    Title = eve.Title,
                    Description= eve.Description,
                    Start = eve.Start.ToString("yyyy-MM-dd"),
                    End = eve.End.ToString("yyyy-MM-dd"),
                    Color = eve.Color,
                    AllDay = eve.AllDay,
                };

                result.Add(resultDto);
            }

            return result;
        }



        public async Task<int> Create(CreateEventDto dto)
        {
            var eventResult = new Event()
            {
                Title = dto.Title,
                Description= dto.Description,
                Start = dto.Date,
                End = dto.Date,
                Color = dto.Color,
                AllDay = dto.AllDay,
                Owner = dto.Owner
            };

            this.dbContext.Events.Add(eventResult);

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return eventResult.Id;
        }

    }
}
