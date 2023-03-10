using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Models
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
        public DbSet<CurrentBind> CurrentBinds { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .Property(u => u.BodyType)
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .IsRequired();

            modelBuilder.Entity<Role>()
              .Property(u => u.Name)
              .IsRequired();

            modelBuilder
              .Entity<Watch>()
              .HasKey(t => t.Id);

            modelBuilder
             .Entity<Watch>()
             .HasOne(c => c.VehicleMany)
             .WithMany(c => c.Bidders)
             .HasForeignKey(cl => cl.VehicleId);

            modelBuilder
             .Entity<Watch>()
             .HasOne(c => c.UserMany)
             .WithMany(c => c.Observed)
             .HasForeignKey(cl => cl.UserId);

            modelBuilder
              .Entity<CurrentBind>()
              .HasKey(t => t.Id);

            modelBuilder
             .Entity<CurrentBind>()
             .HasOne(c => c.VehicleMany)
             .WithMany(c => c.CurrentBinds)
             .HasForeignKey(cl => cl.VehicleId);

            modelBuilder
             .Entity<CurrentBind>()
             .HasOne(c => c.UserMany)
             .WithMany(c => c.CurrentBinds)
             .HasForeignKey(cl => cl.UserId);
        }
    }
}
