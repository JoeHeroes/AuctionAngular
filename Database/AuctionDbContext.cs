using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Watch> Watches { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>()
              .Property(u => u.isFinised)
              .IsRequired();

            modelBuilder.Entity<Bid>()
               .Property(u => u.Id)
               .IsRequired();

            modelBuilder.Entity<Event>()
               .Property(u => u.Start)
               .IsRequired();

            modelBuilder.Entity<Location>()
               .Property(u => u.Name)
               .IsRequired();

            modelBuilder.Entity<Message>()
              .Property(u => u.Email)
              .IsRequired();

            modelBuilder.Entity<Payment>()
              .Property(u => u.LotId)
              .IsRequired();

            modelBuilder.Entity<Picture>()
               .Property(u => u.Id)
               .IsRequired();

            modelBuilder.Entity<Role>()
               .Property(u => u.Name)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .IsRequired();

            modelBuilder.Entity<Vehicle>()
               .Property(u => u.BodyType)
               .IsRequired();

            modelBuilder.Entity<Watch>()
               .Property(u => u.Id)
               .IsRequired();
        }
    }
}
