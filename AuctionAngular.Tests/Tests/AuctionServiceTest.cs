using AuctionAngular.Services;
using AuctionAngularTests;
using Database;
using Microsoft.EntityFrameworkCore;


namespace AuctionAngular.Tests
{
    public class AuctionServiceTest
    {
        private AuctionDbContext _dbContext;

        private AuctionService auctionService;

        public static DbContextOptions<AuctionDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AngularAuctionDB; Trusted_Connection=True";

        static AuctionServiceTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public AuctionServiceTest()
        {
            _dbContext = new AuctionDbContext(dbContextOptions);


            DataTestDBInitializer db = new DataTestDBInitializer();

            db.Seed(_dbContext);

            auctionService = new AuctionService(_dbContext);
        }
    }
}
