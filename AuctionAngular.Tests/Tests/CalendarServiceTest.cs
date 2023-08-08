using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using AuctionAngular.Services;
using AuctionAngularTests;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task GetEventsAsync_ShouldRetrunExecption()
        {
            //Arrange

            //Act

            Func<Task> act = () => calendarService.GetEventsAsync();

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Events not found.", exception.Message);
        }















        [Fact]
        public async Task GetByIdEventAsync_ShouldRetrunEventList()
        {
            //Arrange


            //Act

            var eventResult = await calendarService.GetByIdEventAsync(1);

            //Assert

            Assert.NotNull(eventResult);

        }

        [Fact]
        public async Task GetByIdEventAsync_ShouldRetrunExecption()
        {
            //Arrange

            //Act

            Func<Task> act = () => calendarService.GetByIdEventAsync(999);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Events not found.", exception.Message);
        }
    }
}
