using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Watch> Watches { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
               .Property(u => u.BodyType)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .IsRequired();

            modelBuilder.Entity<Location>()
               .Property(u => u.Name)
               .IsRequired();

            modelBuilder.Entity<Role>()
               .Property(u => u.Name)
               .IsRequired();

            modelBuilder.Entity<Watch>()
               .Property(u => u.Id)
               .IsRequired();


            modelBuilder.Entity<Bid>()
               .Property(u => u.Id)
               .IsRequired();

            modelBuilder.Entity<Picture>()
               .Property(u => u.Id)
               .IsRequired();

        }
    }
}
