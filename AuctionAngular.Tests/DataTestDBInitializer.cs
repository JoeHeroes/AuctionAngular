using Database;


namespace AuctionAngularTests
{
    public class DataTestDBInitializer
    {
        public DataTestDBInitializer()
        {
        }

        public void Seed(AuctionDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}

