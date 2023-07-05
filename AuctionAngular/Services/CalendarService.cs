using Microsoft.EntityFrameworkCore;
using AuctionAngular.Interfaces;
using AuctionAngular.Dtos;
using Database;
using Database.Entities;

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
                    Url = eve.Url
                };

                result.Add(resultDto);
            }

            return result;
        }



        public async Task<ViewEventDto> GetById(int id)
        {
            var eventResult = await this.dbContext
                .Events
                .FirstOrDefaultAsync(u => u.Id == id);

            return  new ViewEventDto()
            {
                Id = eventResult.Id,
                Title = eventResult.Title,
                Description = eventResult.Description,
                Start = eventResult.Start.ToString("yyyy-MM-dd"),
                End = eventResult.End.ToString("yyyy-MM-dd"),
                Color = eventResult.Color,
                AllDay = eventResult.AllDay,
                Url = eventResult.Url
            };
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
                Owner = dto.Owner,
                Url = "/edit-event/"
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

            var updateEventResult = await this.dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventResult.Id);

            updateEventResult!.Url = "/edit-event/" + eventResult.Id;

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

        public async Task Edit(EditEventDto dto)
        {

            var eventResult = await this.dbContext.Events.FirstOrDefaultAsync(x => x.Id == dto.Id);


            eventResult.Title = dto.Title != "" ? dto.Title : eventResult.Title;
            eventResult.Description = dto.Description != "" ? dto.Description : eventResult.Description;
            eventResult.Start = dto.Date.ToString() != default(DateTime).ToString() ? dto.Date : eventResult.Start;
            eventResult.End = dto.Date.ToString() != default(DateTime).ToString() ? dto.Date : eventResult.End;
            eventResult.Color = dto.Color != "" ? dto.Color : eventResult.Color;
            eventResult.AllDay = dto.AllDay;



            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }
    }
}
