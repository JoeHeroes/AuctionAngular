using AuctionAngular.Services;
using AuctionAngularTests;
using Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AuctionAngular.Tests
{
    public class VehicleServiceTest
    {
        private AuctionDbContext _dbContext;
        private Mock<IWebHostEnvironment> _webHost;

        private VehicleService vehicleService;

        public static DbContextOptions<AuctionDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AngularAuctionDB; Trusted_Connection=True";

        static VehicleServiceTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public VehicleServiceTest()
        {
            _dbContext = new AuctionDbContext(dbContextOptions);

            _webHost = new Mock<IWebHostEnvironment>();


            DataTestDBInitializer db = new DataTestDBInitializer();

            db.Seed(_dbContext);

            vehicleService = new VehicleService(_dbContext, _webHost.Object);
        }









    }
}
