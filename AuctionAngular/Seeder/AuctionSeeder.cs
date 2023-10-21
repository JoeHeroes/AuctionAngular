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
                    isInvoiceGenereted = false,
                }
            };
        }
        public IEnumerable<Auction> GetAuction()
        {
            return new List<Auction>()
            {
                new Auction()
                {
                    DateTime = DateTime.Now.AddDays(1),
                    LocationId = 1,
                    Description = "",
                    isStarted = false,
                    isFinised = false,
                    
                },
                new Auction()
                {
                    DateTime = DateTime.Now.AddDays(2),
                    LocationId = 2,
                    Description = "",
                    isStarted = false,
                    isFinised = false,
                },
                new Auction()
                {
                    DateTime = DateTime.Now.AddDays(3),
                    LocationId = 3,
                    Description = "",
                    isStarted = false,
                    isFinised = false,
                },

            };
        }

        public IEnumerable<Location> GetLocations()
        {
            return new List<Location>()
            {
                new Location()
                {
                    Name = "Warsaw",
                    Phone = "358401776000",
                    Email = "Warsaw@Poland.pl",
                    City = "Warsaw",
                    Street = "Warsaw 7",
                    PostalCode = "Warsaw 02920",
                    County = "Poland",
                },
                new Location()
                {
                    Name = "Berlin",
                    Phone = "358401776000",
                    Email = "Berlin@German.de",
                    City = "Berlin",
                    Street = "Berlin 1",
                    PostalCode = "Berlin 90940",
                    County = "German",
                },
                new Location()
                {
                    Name = "Praga",
                    Phone = "358401776000",
                    Email = "Praga@Czech.cz",
                    City = "Praga",
                    Street = "Praga 24",
                    PostalCode = "Praga 33960",
                    County = "Czech",
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

                //Audi
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.1.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.2.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.3.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.4.jpg"
                },new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.5.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.6.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.7.jpg"
                }, 
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.8.jpg"
                }, 
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.9.jpg"
                },
                new Picture()
                {
                    VehicleId = 1,
                    PathImg = "A4.10.jpg"
                },

                //Ford
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.1.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.2.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.3.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.4.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.5.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.6.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.7.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.8.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.9.jpg"
                },
                new Picture()
                {
                    VehicleId = 2,
                    PathImg = "Ford.10.jpg"
                },


                //Volkswagen
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.1.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.2.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.3.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.4.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.5.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.6.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.7.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.8.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.9.jpg"
                },
                new Picture()
                {
                    VehicleId = 3,
                    PathImg = "Volkswagen.10.jpg"
                },


                //Nissan
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.1.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.2.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.3.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.4.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.5.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.6.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.7.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.8.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.9.jpg"
                },
                new Picture()
                {
                    VehicleId = 4,
                    PathImg = "Nissan.10.jpg"
                },


                 //Mercedes
                new Picture()
                {
                    VehicleId = 5,
                    PathImg = "Mercedes.1.jpg"
                },
                new Picture()
                {
                    VehicleId = 5,
                    PathImg = "Mercedes.2.jpg"
                },
                new Picture()
                {
                    VehicleId = 5,
                    PathImg = "Mercedes.3.jpg"
                },
                new Picture()
                {
                    VehicleId = 5,
                    PathImg = "Mercedes.4.jpg"
                },
                new Picture()
                {
                    VehicleId = 5,
                    PathImg = "Mercedes.5.jpg"
                },
                new Picture()
                {
                    VehicleId = 5,
                    PathImg = "Mercedes.6.jpg"
                },
                new Picture()
                {
                    VehicleId = 5,
                    PathImg = "Mercedes.7.jpg"
                },


                //Chevrolet
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.1.jpg"
                },
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.2.jpg"
                },
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.3.jpg"
                },
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.4.jpg"
                },
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.5.jpg"
                },
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.6.jpg"
                },
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.7.jpg"
                },
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.8.jpg"
                },
                new Picture()
                {
                    VehicleId = 6,
                    PathImg = "Chevrolet.9.jpg"
                },


                //Chevrolet
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.1.jpg"
                },
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.2.jpg"
                },
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.3.jpg"
                },
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.4.jpg"
                },
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.5.jpg"
                },
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.6.jpg"
                },
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.7.jpg"
                },
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.8.jpg"
                },
                new Picture()
                {
                    VehicleId = 7,
                    PathImg = "BMW.9.jpg"
                },

            };
        }



        public IEnumerable<Vehicle> GetVehicle()
        {
            return new List<Vehicle>()
            {
                new Vehicle()
                {
                    Producer = Producer.Audi.ToString(),
                    ModelSpecifer = "A4",
                    ModelGeneration = "B9",
                    RegistrationYear = 2016,
                    Color = "Gray",
                    BodyType = BodyCar.Sedan.ToString(),
                    EngineCapacity = 1998,
                    EngineOutput = 110,
                    Transmission = Transmission.DualClutch.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 180000,
                    Fuel = Fuel.Diesel.ToString(),
                    NumberKeys = 2,
                    ServiceManual = true,
                    SecondTireSet = true,
                    CurrentBid = 0,
                    PrimaryDamage = Damage.Front_End.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    VIN = "XXXX",
                    AuctionId = 3,
                    SaleTerm="",
                    Category="",
                    OwnerId = 1,
                },
                new Vehicle()
                {
                    Producer = Producer.Ford.ToString(),
                    ModelSpecifer = "CMAX",
                    ModelGeneration = "2",
                    RegistrationYear = 2015,
                    Color = "Brown",
                    BodyType = BodyCar.Van.ToString(),
                    EngineCapacity = 1999,
                    EngineOutput = 110,
                    Transmission = Transmission.Manual.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 25000,
                    Fuel = Fuel.Petrol.ToString(),
                    NumberKeys = 2,
                    ServiceManual = true,
                    SecondTireSet = true,
                    CurrentBid = 0,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    VIN = "XXXX",
                    AuctionId = 1,
                    SaleTerm="",
                    Category="",
                    OwnerId = 1,
                },
                new Vehicle()
                {
                    Producer = Producer.Volkswagen.ToString(),
                    ModelSpecifer = "Polo",
                    ModelGeneration = "5",
                    RegistrationYear = 2017,
                    Color = "Red",
                    BodyType = BodyCar.Hatchback.ToString(),
                    EngineCapacity = 999,
                    EngineOutput = 88,
                    Transmission = Transmission.Manual.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 230000,
                    Fuel = Fuel.Diesel.ToString(),
                    NumberKeys = 2,
                    ServiceManual = true,
                    SecondTireSet = true,
                    CurrentBid = 0,
                    PrimaryDamage = Damage.Vandalism.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    VIN = "XXXX",
                    AuctionId = 3,
                    SaleTerm="",
                    Category="",
                    OwnerId = 1,
                },
                new Vehicle()
                {
                    Producer = Producer.Nissan.ToString(),
                    ModelSpecifer = "Note",
                    ModelGeneration = "2",
                    RegistrationYear = 2016,
                    Color = "Gray",
                    BodyType = BodyCar.Hatchback.ToString(),
                    EngineCapacity = 999,
                    EngineOutput = 88,
                    Transmission = Transmission.Manual.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 230000,
                    Fuel = Fuel.Diesel.ToString(),
                    NumberKeys = 2,
                    ServiceManual = true,
                    SecondTireSet = true,
                    CurrentBid = 0,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    VIN = "XXXX",
                    AuctionId = 3,
                    SaleTerm="",
                    Category="",
                    OwnerId = 1,
                },

                new Vehicle()
                {
                    Producer = Producer.Mercedes.ToString(),
                    ModelSpecifer = "S",
                    ModelGeneration = "140",
                    RegistrationYear = 1994,
                    Color = "Black",
                    BodyType = BodyCar.Sedan.ToString(),
                    EngineCapacity = 5980,
                    EngineOutput = 288,
                    Transmission = Transmission.Automatic.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 7890000,
                    Fuel = Fuel.Petrol.ToString(),
                    NumberKeys = 1,
                    ServiceManual = false,
                    SecondTireSet = true,
                    CurrentBid = 0,
                    PrimaryDamage = Damage.Minor_Dents_Scratch.ToString(),
                    SecondaryDamage = Damage.Normal_Wear.ToString(),
                    VIN = "XXXX",
                    AuctionId = 2,
                    SaleTerm="",
                    Category="",
                    OwnerId = 1,
                },

                new Vehicle()
                {
                    Producer = Producer.Chevrolet.ToString(),
                    ModelSpecifer = "Corvette",
                    ModelGeneration = "C6",
                    RegistrationYear = 1997,
                    Color = "Yellow",
                    BodyType = BodyCar.Coupe.ToString(),
                    EngineCapacity = 7380,
                    EngineOutput = 288,
                    Transmission = Transmission.Automatic.ToString(),
                    Drive = Drive.FWD.ToString(),
                    MeterReadout = 56000,
                    Fuel = Fuel.Petrol.ToString(),
                    NumberKeys = 1,
                    ServiceManual = false,
                    SecondTireSet = true,
                    CurrentBid = 0,
                    PrimaryDamage = Damage.Front_End.ToString(),
                    SecondaryDamage = Damage.All_Over.ToString(),
                    VIN = "XXXX",
                    AuctionId = 2,
                    SaleTerm="",
                    Category="",
                    OwnerId = 1,
                },


                new Vehicle()
                {
                    Producer = Producer.BMW.ToString(),
                    ModelSpecifer = "6",
                    ModelGeneration = "E24",
                    RegistrationYear = 1984,
                    Color = "White",
                    BodyType = BodyCar.Coupe.ToString(),
                    EngineCapacity = 3480,
                    EngineOutput = 588,
                    Transmission = Transmission.Automatic.ToString(),
                    Drive = Drive.RWD.ToString(),
                    MeterReadout = 156000,
                    Fuel = Fuel.Petrol.ToString(),
                    NumberKeys = 1,
                    ServiceManual = false,
                    SecondTireSet = true,
                    CurrentBid = 0,
                    PrimaryDamage = Damage.Normal_Wear.ToString(),
                    SecondaryDamage = Damage.All_Over.ToString(),
                    VIN = "XXXX",
                    AuctionId = 3,
                    SaleTerm="",
                    Category="",
                    OwnerId = 1,
                },
            };
        }
    }
}