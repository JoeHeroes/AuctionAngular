using AuctionAngular.Enum;
using AuctionAngular.Models;
using CarAuction.Models;
using CarAuction.Models.Enum;

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

                if (!_dbContext.Roles.Any())
                {
                    var info = GetRole();
                    _dbContext.Roles.AddRange(info);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Vehicles.Any())
                {
                    var info = GetInfoVehicle();
                    _dbContext.Vehicles.AddRange(info);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Locations.Any())
                {
                    var info = GetLocations();
                    _dbContext.Locations.AddRange(info);
                    _dbContext.SaveChanges();
                }
            }
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
                    ProfileImg = "Espoo.png"
                },
                new Location()
                {
                    Name = "Oulu",
                    Phone = "358401776000",
                    Email = "Oulu@Copart.fi",
                    City = "Oulu",
                    Street = "Ahertajantie 1",
                    PostalCode = "North Ostrobothnia 90940",
                    ProfileImg = "Oulu.png"
                },
                new Location()
                {
                    Name = "Pirkkala",
                    Phone = "358401776000",
                    Email = "Pirkkala@Copart.fi",
                    City = "Pirkkala",
                    Street = "Teollisuustie 24",
                    PostalCode = "Pirkanmaa 33960",
                    ProfileImg = "Pirkkala.png"
                },
                new Location()
                {
                    Name = "Turku",
                    Phone = "358401776000",
                    Email = "Turku@Copart.fi",
                    City = "Turku",
                    Street = "Tiemestarinkatu 5",
                    PostalCode = "20360",
                    ProfileImg = "Turku.png"
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

        public IEnumerable<Vehicle> GetInfoVehicle()
        {
            return new List<Vehicle>()
            {
                new Vehicle()
                {
                    Producer = Producer.Audi.ToString(),
                    ModelSpecifer = "A4",
                    ModelGeneration = "B9",
                    RegistrationYear = 2016,
                    Color = "Grey",
                    BodyType = BodyCar.Combi.ToString(),
                    EngineCapacity = 1968 ,
                    EngineOutput = 110,
                    Transmission = Transmission.DualClutch.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 177000,
                    Fuel = Fuel.Diesel.ToString(),
                    NumberKeys = "1",
                    ServiceManual = true,
                    SecondTireSet = true,
                    Watch = false,
                    CurrentBid = 5000,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    DateTime = new DateTime(2023,12,28),
                    VIN = "",
                    ProfileImg = "a4b9.png",
                    Location = new Location()
                },
                new Vehicle()
                {
                    Producer = Producer.Audi.ToString(),
                    ModelSpecifer = "A4",
                    ModelGeneration = "B6",
                    RegistrationYear = 2004,
                    Color = "Blue",
                    BodyType = BodyCar.Sedan.ToString(),
                    EngineCapacity = 1896,
                    EngineOutput = 96,
                    Transmission = Transmission.Manual.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 268000,
                    Fuel = Fuel.Diesel.ToString(),
                    NumberKeys = "1",
                    ServiceManual = true,
                    SecondTireSet = false,
                    Watch = false,
                    CurrentBid = 5000,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    DateTime = new DateTime(2023,12,18),
                    VIN = "",
                    ProfileImg = "a4b6.png",
                    Location = new Location()

                },
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
                    NumberKeys = "2",
                    ServiceManual = true,
                    SecondTireSet = true,
                    Watch = false,
                    CurrentBid = 5000,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    DateTime = new DateTime(2023,12,16 ),
                    VIN = "",
                    ProfileImg = "passatb8.png",
                    Location = new Location()
                },
                new Vehicle()
                {
                    Producer = Producer.Skoda.ToString(),
                    ModelSpecifer = "Octavia",
                    ModelGeneration = "3",
                    RegistrationYear = 2015,
                    Color = "LightBlue",
                    BodyType = BodyCar.Liftback.ToString(),
                    EngineCapacity = 1968,
                    EngineOutput = 110,
                    Transmission = Transmission.DualClutch.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 172000,
                    Fuel = Fuel.Diesel.ToString(),
                    NumberKeys = "2",
                    ServiceManual = true,
                    SecondTireSet = true,
                     Watch = false,
                    CurrentBid = 5000,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    DateTime = new DateTime(2023,12,30),
                    VIN = "",
                    ProfileImg = "octavia3.png",
                    Location = new Location()
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
                    NumberKeys = "2",
                    ServiceManual = true,
                    SecondTireSet = true,
                    Watch = false,
                    CurrentBid = 5000,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    DateTime = new DateTime(2023,1,1),
                    VIN = "",
                    ProfileImg = "polo2g.png",
                    Location = new Location()
                },
                new Vehicle()
                {
                    Producer = Producer.Toyota.ToString(),
                    ModelSpecifer = "Yaris",
                    ModelGeneration = "2",
                    RegistrationYear = 2010,
                    Color = "White",
                    BodyType = BodyCar.Hatchback.ToString(),
                    EngineCapacity = 1364,
                    EngineOutput = 66,
                    Transmission = Transmission.Manual.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 80000,
                    Fuel = Fuel.Diesel.ToString(),
                    NumberKeys = "2",
                    ServiceManual = false,
                    SecondTireSet = false,
                    Watch = false,
                    CurrentBid = 5000,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    DateTime = new DateTime(2023,1,4),
                    VIN = "",
                    ProfileImg = "yaris2.png",
                    Location = new Location()
                }
            };
        }
    }
}