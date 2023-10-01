using Database;
using Database.Entities;

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

            context.Locations.AddRange(new Location()
                {
                    Name = "Espoo",
                    Phone = "358401776000",
                    Email = "Espoo@Copart.fi",
                    City = "Espoo",
                    Street = "Pieni teollisuuskatu 7",
                    PostalCode = "Uusimaa 02920",
                    County = "Finalndia"
                }
            );

            context.Auctions.AddRange(new Auction
            {
                DateTime = DateTime.Now.AddDays(1),
                LocationId = 1,
                Description = "",
                SalesStarted = false,
                SalesFinised = false,
            },
            new Auction
            {
                DateTime = DateTime.Now.AddDays(2),
                LocationId = 1,
                Description = "",
                SalesStarted = false,
                SalesFinised = false,
            });


            context.Bids.AddRange(new Bid()
            {
                UserId = 1,
                VehicleId = 1,
            },
            new Bid()
            {
                UserId = 1,
                VehicleId = 2,
            });

            context.Vehicles.AddRange(new Vehicle()
            {
                Producer = "Test",
                ModelSpecifer = "Test",
                ModelGeneration = "Test",
                RegistrationYear = 2000,
                Color = "Test",
                BodyType = "Test",
                EngineCapacity = 999,
                EngineOutput = 999,
                Transmission = "Test",
                Drive = "Test",
                MeterReadout = 1000,
                Fuel = "Test",
                NumberKeys = 1,
                ServiceManual = true,
                SecondTireSet = true,
                CurrentBid = 1000,
                PrimaryDamage = "Test",
                SecondaryDamage = "Test",
                VIN = "Test",
                AuctionId = 1,
                SaleTerm = "Test",
                Highlights = "Test",
            },
            new Vehicle()
            {
                Producer = "Test",
                ModelSpecifer = "Test",
                ModelGeneration = "Test",
                RegistrationYear = 2000,
                Color = "Test",
                BodyType = "Test",
                EngineCapacity = 999,
                EngineOutput = 999,
                Transmission = "Test",
                Drive = "Test",
                MeterReadout = 1000,
                Fuel = "Test",
                NumberKeys = 1,
                ServiceManual = true,
                SecondTireSet = true,
                CurrentBid = 1000,
                PrimaryDamage = "Test",
                SecondaryDamage = "Test",
                VIN = "Test",
                AuctionId = 2,
                SaleTerm = "Test",
                Highlights = "Test",
            });

            context.Locations.AddRange(new Location()
            {
                Name = "Test1",
                Phone = "Test2",
                Email = "Test@Test.pl",
                City = "Test3",
                Street = "Test4",
                PostalCode = "Test5",
                County = "Test6"
            });


            context.Events.AddRange(new Event()
            {
                Title = "Test",
                Description = "Test",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                Color = "Red",
                isAllDay = false,
                UserId = 1,
                Url = "/edit-event/"
            });

            context.Roles.AddRange(
                 new Role()
                 {
                     Name = "Buyer"
                 });

            context.Users.AddRange(
                new User() {
                    Email = "Test2@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture="",
                    isConfirmed = false
                },
                new User()
                {
                    Email = "Test2@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                    isConfirmed = true
                },
                new User()
                {
                    Email = "Test7@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                    isConfirmed = false
                },
                new User()
                {
                    Email = "Test9@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                    isConfirmed = true
                },
                new User()
                {
                    Email = "Test10@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                   isConfirmed = true
                },
                new User()
                {
                    Email = "Test11@wpl",
                    PasswordHash = "Password12#",
                    Name = "Test",
                    SureName = "Test",
                    Nationality = "Poland",
                    Phone = "+48 123 456 789",
                    DateOfBirth = DateTime.Now.Date,
                    RoleId = 1,
                    ProfilePicture = "",
                    isConfirmed = true
                }
            );


            context.SaveChanges();
        }
    }
}

