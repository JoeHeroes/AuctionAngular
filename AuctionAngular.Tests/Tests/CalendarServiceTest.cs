using AuctionAngular.Dtos;
using AuctionAngular.Services;
using AuctionAngularTests;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Tests
{
    public class CalendarServiceTest
    {
        private AuctionDbContext _dbContext;

        private CalendarService calendarService;

        public static DbContextOptions<AuctionDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AngularAuctionDB; Trusted_Connection=True";

        static CalendarServiceTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public CalendarServiceTest()
        {
            _dbContext = new AuctionDbContext(dbContextOptions);


            DataTestDBInitializer db = new DataTestDBInitializer();

            db.Seed(_dbContext);

            calendarService = new CalendarService(_dbContext);
        }

        [Fact]
        public async Task GetEventsAsync_ShouldRetrunEventList()
        {
            //Arrange


            //Act

            var events = await calendarService.GetEventsAsync();

            //Assert

            Assert.NotNull(events);
            Assert.True(events.Count() > 0);
            
        }


        [Fact]
        public async Task GetByIdEventAsync_ShouldRetrunEventList()
        {
            //Arrange

            var eventExaple = new Event()
            {
                Title = "Test",
                Description = "Test",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                Color = "Red",
                AllDay = false,
                Owner = 1,
                Url = "/edit-event/"
            };

            //Act

            var eventResult = await calendarService.GetByIdEventAsync(1);

            //Assert

            Assert.NotNull(eventResult);
            Assert.Equal(eventExaple.Title, eventResult.Title);
            Assert.Equal(eventExaple.Description, eventResult.Description);
            Assert.Equal(eventExaple.Color, eventResult.Color);
            Assert.Equal(eventExaple.AllDay, eventResult.AllDay);


        }

        [Fact]
        public async Task GetByIdEventAsync_ShouldRetrunExecption()
        {
            //Arrange

            //Act

            Func<Task> act = () => calendarService.GetByIdEventAsync(999);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Event not found.", exception.Message);
        }





        


        [Fact]
        public async Task CreateEventAsync_WithValidData_ShouldReturId()
        {
            //Arrange

            var eventResult = new CreateEventDto
            {
                Title = "Test",
                Description = "Test",
                Date = new DateTime(),
                Color = "red",
                AllDay = false,
                Owner = 1
            };

            //Act

            var id = await calendarService.CreateEventAsync(eventResult);

            //Assert

            Assert.NotEqual(0, id);
        }



        [Fact]
        public async Task EditEventsAsync_WithValidData_ShouldEditEvent()
        {
            //Arrange

            var eventEdit = new EditEventDto
            {
                Id = 1,
                Title = "Test1",
                Description = "Test1",
                Date = new DateTime().AddHours(1),
                Color = "blue",
                AllDay = true,
            };

            //Act

            var eventResult = await calendarService.EditEventAsync(eventEdit);

            //Assert
            Assert.NotNull(eventResult);
            Assert.Equal(eventEdit.Title, eventResult.Title);
            Assert.Equal(eventEdit.Description, eventResult.Description);
            Assert.Equal(eventEdit.Color, eventResult.Color);
            Assert.Equal(eventEdit.AllDay, eventResult.AllDay);
        }



        [Fact]
        public async Task EditEventsAsync_WithInvalidData_ShouldRetrunExecption()
        {
            //Arrange

            var eventEdit = new EditEventDto
            {
                Id = 99,
                Title = "Test1",
                Description = "Test1",
                Date = new DateTime().AddHours(1),
                Color = "blue",
                AllDay = true,
            };

            //Act

            Func<Task> act = () => calendarService.EditEventAsync(eventEdit);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Event not found.", exception.Message);
        }

        [Fact]
        public async Task DeleteEventsAsync_WithValidData_ShouldDeleteEvent()
        {
            //Arrange

            //Act

            var id = await calendarService.DeleteEventAsync(1);

            //Assert
            Assert.NotEqual(0, id);

        }

        [Fact]
        public async Task DeleteEventsAsync_WithInvalidData_ShouldRetrunExecption()
        {
            //Arrange

            //Act

            Func<Task> act = () => calendarService.DeleteEventAsync(99);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Event not found.", exception.Message);
        }
    }
}
