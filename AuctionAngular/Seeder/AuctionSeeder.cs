using AuctionAngular.Enums;
using Database;
using Database.Entities;

namespace CarAuction.Seeder
{
    public class AuctionSeeder
    {
        private readonly AuctionDbContext _dbContext;

        public AuctionSeeder(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {

                if (!_dbContext.Payments.Any())
                {
                    var info = GetPayment();
                    _dbContext.Payments.AddRange(info);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Roles.Any())
                {
                    var info = GetRole();
                    _dbContext.Roles.AddRange(info);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Vehicles.Any())
                {
                    var info = GetVehicle();
                    _dbContext.Vehicles.AddRange(info);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Auctions.Any())
                {
                    var info = GetAuction();
                    _dbContext.Auctions.AddRange(info);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Locations.Any())
                {
                    var info = GetLocations();
                    _dbContext.Locations.AddRange(info);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Pictures.Any())
                {
                    var pic = GetPictures();
                    _dbContext.Pictures.AddRange(pic);
                    _dbContext.SaveChanges();
                }
            }
        }

        public IEnumerable<Payment> GetPayment()
        {
            return new List<Payment>()
            {
                new Payment
                {
                    SaleDate = DateTime.Now,
                    LotId = 1,
                    LocationId = 1,
                    Description = "XD",
                    InvoiceAmount = 1231,
                    LastInvoicePaidDate = DateTime.Now.AddDays(1),
                    LotLeftLocationDate =  DateTime.Now.AddDays(2),
                    StatusSell = false,
                    InvoiceGenereted = false,
                }
            };
        }
        public IEnumerable<Auction> GetAuction()
        {
            return new List<Auction>()
            {
                new Auction()
                {
                    DateTime = DateTime.Now,
                    LocationId = 1,
                    SalesStarted = false,
                    SalesFinised = false,
                    
                },
                new Auction()
                {
                    DateTime = new DateTime(2023, 8, 7, 19, 0, 0),
                    LocationId = 2,
                    SalesStarted = false,
                    SalesFinised = false,
                },
                new Auction()
                {
                    DateTime = new DateTime(2023, 8, 7, 19, 0, 0),
                    LocationId = 3,
                    SalesStarted = false,
                    SalesFinised = false,
                },

            };
        }

        public IEnumerable<Location> GetLocations()
        {
            return new List<Location>()
            {
                new Location()
                {
                    Name = "Espoo",
                    Phone = "358401776000",
                    Email = "Espoo@Copart.fi",
                    City = "Espoo",
                    Street = "Pieni teollisuuskatu 7",
                    PostalCode = "Uusimaa 02920",
                    County = "Finlandia",
                    Picture = "Espoo.png"
                },
                new Location()
                {
                    Name = "Oulu",
                    Phone = "358401776000",
                    Email = "Oulu@Copart.fi",
                    City = "Oulu",
                    Street = "Ahertajantie 1",
                    PostalCode = "North Ostrobothnia 90940",
                    County = "Finlandia",
                    Picture = "Oulu.png"
                },
                new Location()
                {
                    Name = "Pirkkala",
                    Phone = "358401776000",
                    Email = "Pirkkala@Copart.fi",
                    City = "Pirkkala",
                    Street = "Teollisuustie 24",
                    PostalCode = "Pirkanmaa 33960",
                    County = "Finlandia",
                    Picture = "Pirkkala.png"
                },
                new Location()
                {
                    Name = "Turku",
                    Phone = "358401776000",
                    Email = "Turku@Copart.fi",
                    City = "Turku",
                    Street = "Tiemestarinkatu 5",
                    PostalCode = "20360",
                    County = "Finlandia",
                    Picture = "Turku.png"
                },
            };
        }

        public IEnumerable<Role> GetRole()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name = "Buyer"
                },
                new Role()
                {
                    Name = "Seller"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };
        }

        public IEnumerable<Picture> GetPictures()
        {
            return new List<Picture>()
            {
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "passat1.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "passat2.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "passat3.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "passat4.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "passat5.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "polo1.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "polo2.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "polo3.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "polo4.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "polo5.jpg"
                },
            };
        }



        public IEnumerable<Vehicle> GetVehicle()
        {
            return new List<Vehicle>()
            {
                new Vehicle()
                {
                    Producer = Producer.Volkswagen.ToString(),
                    ModelSpecifer = "Passat",
                    ModelGeneration = "B8",
                    RegistrationYear = 2015,
                    Color = "Black",
                    BodyType = BodyCar.Combi.ToString(),
                    EngineCapacity = 1598,
                    EngineOutput = 88,
                    Transmission = Transmission.DualClutch.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 180000,
                    Fuel = Fuel.Diesel.ToString(),
                    NumberKeys = 2,
                    ServiceManual = true,
                    SecondTireSet = true,
                    CurrentBid = 5000,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    VIN = "XXXX",
                    AuctionId = 3,
                    SaleTerm="",
                    Highlights="",
                },
                new Vehicle()
                {
                    Producer = Producer.Volkswagen.ToString(),
                    ModelSpecifer = "Polo",
                    ModelGeneration = "2G",
                    RegistrationYear = 2020,
                    Color = "White",
                    BodyType = BodyCar.Hatchback.ToString(),
                    EngineCapacity = 999,
                    EngineOutput = 59,
                    Transmission = Transmission.Manual.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 25000,
                    Fuel = Fuel.Petrol.ToString(),
                    NumberKeys = 2,
                    ServiceManual = true,
                    SecondTireSet = true,
                    CurrentBid = 5000,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    VIN = "XXXX",
                    AuctionId = 1,
                    SaleTerm="",
                    Highlights="",
                },
            };
        }
    }
}