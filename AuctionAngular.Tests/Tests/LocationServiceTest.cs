using AuctionAngular.Interfaces;
using AuctionAngular.Services;
using AuctionAngularTests;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace AuctionAngular.Tests
{
    public class LocationServiceTest
    {
        private AuctionDbContext _dbContext;

        private LocationService locationService;

        public static DbContextOptions<AuctionDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AngularAuctionDB; Trusted_Connection=True";

        static LocationServiceTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public LocationServiceTest()
        {
            _dbContext = new AuctionDbContext(dbContextOptions);


            DataTestDBInitializer db = new DataTestDBInitializer();

            db.Seed(_dbContext);

            locationService = new LocationService(_dbContext);
        }

        [Fact]
        public async Task GetLocationsAsync_ShouldRetrunLocationList()
        {
            //Arrange


            //Act

            var Locations = await locationService.GetLocationsAsync();

            //Assert

            Assert.NotNull(Locations);
            Assert.True(Locations.Count() > 0);

        }

        [Fact]
        public async Task GetByIdLocationAsync_WithValidData_ShouldRetrunEvent()
        {
            //Arrange

            var locationExaple = new Location()
            {
                Name = "Test1",
                Phone = "Test2",
                Email = "Test@Test.pl",
                City = "Test3",
                Street = "Test4",
                PostalCode = "Test5",
                Picture = "Test6"
            };

            //Act

            var locationResult = await locationService.GetByIdLocationAsync(1);

            //Assert

            Assert.NotNull(locationResult);
            Assert.Equal(locationResult.Name, locationExaple.Name);
            Assert.Equal(locationResult.Phone, locationExaple.Phone);
            Assert.Equal(locationResult.Email, locationExaple.Email);
            Assert.Equal(locationResult.Street, locationExaple.Street);
            Assert.Equal(locationResult.PostalCode, locationExaple.PostalCode);
            Assert.Equal(locationResult.Picture, locationExaple.Picture);

        }


        [Fact]
        public async Task GetByIdEventAsync_WithInvalidData_ShouldRetrunExecption()
        {
            //Arrange

            //Act

            Func<Task> act = () => locationService.GetByIdLocationAsync(99);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Location not found.", exception.Message);

        }
    }
}
