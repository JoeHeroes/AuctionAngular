using Microsoft.EntityFrameworkCore;
using AuctionAngular.Interfaces;
using AuctionAngular.Dtos;
using Database;
using Database.Entities;

namespace AuctionAngular.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly AuctionDbContext _dbContext;
        /// <inheritdoc/>
        public CalendarService(AuctionDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
  
        public async Task<IEnumerable<ViewEventDto>> GetEventsAsync(int userId)
        {
            var events = await _dbContext
                .Events
                .ToListAsync();

            if (events is null)
            {
                throw new NotFoundException("Events not found.");
            }

            List<ViewEventDto> result = new List<ViewEventDto>();

            foreach(var eve in events)
            {
                if (eve.Owner == userId || eve.Owner == 0)
                {
                    var resultDto = new ViewEventDto()
                    {
                        Id = eve.Id,
                        Title = eve.Title,
                        Description = eve.Description,
                        Start = eve.Start.ToString("yyyy-MM-dd"),
                        End = eve.End.ToString("yyyy-MM-dd"),
                        Color = eve.Color,
                        AllDay = eve.AllDay,
                        Url = eve.Url
                    };

                    result.Add(resultDto);
                }
            }

            return result;
        }

        public async Task<ViewEventDto> GetByIdEventAsync(int id)
        {
            var eventResult = await _dbContext
                .Events
                .FirstOrDefaultAsync(u => u.Id == id);

            if (eventResult is null)
            {
                throw new NotFoundException("Event not found.");
            }

            return new ViewEventDto()
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

        public async Task<int> CreateEventAsync(CreateEventDto dto)
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
                Url = "/event/edit/"
            };

            await _dbContext.Events.AddAsync(eventResult);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            var updateEventResult = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventResult.Id);

            updateEventResult!.Url = "/event/edit/" + eventResult.Id;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return eventResult.Id;
        }

        public async Task<Event> EditEventAsync(EditEventDto dto)
        {

            var eventResult = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (eventResult is null)
            {
                throw new NotFoundException("Event not found.");
            }

            eventResult.Title = dto.Title != "" ? dto.Title : eventResult.Title;
            eventResult.Description = dto.Description != "" ? dto.Description : eventResult.Description;
            eventResult.Start = dto.Date.ToString() != default(DateTime).ToString() ? dto.Date : eventResult.Start;
            eventResult.End = dto.Date.ToString() != default(DateTime).ToString() ? dto.Date : eventResult.End;
            eventResult.Color = dto.Color != "" ? dto.Color : eventResult.Color;
            eventResult.AllDay = dto.AllDay;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return eventResult;
        }

        public async Task<int> DeleteEventAsync(int id)
        {

            var eventResult = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);

            if(eventResult == null)
            {
                throw new NotFoundException("Event not found.");
            }

            _dbContext.Events.Remove(eventResult);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return id;
        }
    }
}
