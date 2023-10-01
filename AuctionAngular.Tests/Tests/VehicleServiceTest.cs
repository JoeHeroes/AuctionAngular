using AuctionAngular.Dtos.Bid;
using AuctionAngular.Dtos.Vehicle;
using AuctionAngular.Dtos.Watch;
using AuctionAngular.Enums;
using AuctionAngular.Services;
using AuctionAngularTests;
using Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace AuctionAngular.Tests
{
    public class VehicleServiceTest
    {
        private AuctionDbContext _dbContext;
        private Mock<IWebHostEnvironment> _webHost;

        private VehicleService vehicleService;

        public static DbContextOptions<AuctionDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AngularAuctionDBTest4; Trusted_Connection=True";

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


        [Fact]
        public async Task GetByIdVehicleAsync_WithValidData_ShouldReturnVehicle()
        {
            //Arrange

            var dto = new ViewVehicleDto()
            {
                Id = 1,
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
                AuctionId = 1,
                PrimaryDamage = "Test",
                SecondaryDamage = "Test",
                VIN = "Test",
                Highlights = "Test",
                CurrentBid = 1000,
                WinnerId = 1,
                isSold = false,
            };

            //Act

            var vehicle = await vehicleService.GetByIdVehicleAsync(dto.Id);

            //Assert

            Assert.NotNull(vehicle);
            Assert.Equal(dto.Producer, vehicle.ModelSpecifer);
            Assert.Equal(dto.ModelSpecifer, vehicle.ModelSpecifer);
            Assert.Equal(dto.ModelGeneration, vehicle.ModelGeneration);
            Assert.Equal(dto.RegistrationYear, vehicle.RegistrationYear);
            Assert.Equal(dto.Color, vehicle.Color);
            Assert.Equal(dto.BodyType, vehicle.BodyType);
            Assert.Equal(dto.EngineCapacity, vehicle.EngineCapacity);
            Assert.Equal(dto.EngineOutput, vehicle.EngineOutput);
            Assert.Equal(dto.Transmission, vehicle.Transmission);
            Assert.Equal(dto.Drive, vehicle.Drive);
            Assert.Equal(dto.MeterReadout, vehicle.MeterReadout);
            Assert.Equal(dto.Fuel, vehicle.Fuel);
            Assert.Equal(dto.NumberKeys, vehicle.NumberKeys);
            Assert.Equal(dto.ServiceManual, vehicle.ServiceManual);
            Assert.Equal(dto.SecondTireSet, vehicle.SecondTireSet);
            Assert.Equal(dto.AuctionId, vehicle.AuctionId);
            Assert.Equal(dto.PrimaryDamage, vehicle.PrimaryDamage);
            Assert.Equal(dto.SecondaryDamage, vehicle.SecondaryDamage);
            Assert.Equal(dto.VIN, vehicle.VIN);
            Assert.Equal(dto.Highlights, vehicle.Highlights);
            Assert.Equal(dto.CurrentBid, vehicle.CurrentBid);
        }

        [Fact]
        public async Task CreateUserAsync_WithInvalidData_ShouldReturnException()
        {
            //Arrange

            //Act

            Func<Task> act = () => vehicleService.GetByIdVehicleAsync(99);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Vehicle not found.", exception.Message);
        }



        [Fact]
        public async Task GetVehiclesAsync_WithValidData_ShouldReturnVehiclesList()
        {
            //Arrange

            //Act

            var vehicles = await vehicleService.GetVehiclesAsync(true);

            //Assert
            Assert.True(vehicles.IsNullOrEmpty());
        }



        [Fact]
        public async Task GetAllBidedAsync_WithValidData_ShouldReturnBids()
        {
            //Arrange


            //Act

            var bids = await vehicleService.GetAllBidedAsync(1);

            //Assert

            Assert.True(bids.IsNullOrEmpty());
        }


        [Fact]
        public async Task GetAllBidedAsync_WithInvalidData_ShouldReturnException()
        {
            //Arrange

            //Act

            var bids = await vehicleService.GetAllBidedAsync(99);

            //Assert

            Assert.True(bids.IsNullOrEmpty());
        }

        [Fact]
        public async Task GetAllLostAsync_WithInvalidData_ShouldNull()
        {
            //Arrange

            //Act

            var bids = await vehicleService.GetAllWonAsync(2);

            //Assert

            Assert.True(bids.IsNullOrEmpty());
        }

        [Fact]
        public async Task CreateVehicleAsync_WithValidData_ShouldCreateVehicle()
        {
            //Arrange

            var dto = new CreateVehicleDto()
            {
                Producer = "test",
                ModelSpecifer = "test",
                ModelGeneration = "test",
                RegistrationYear = 2000,
                Color = "test",
                BodyType = "test",
                EngineCapacity = 999,
                EngineOutput = 999,
                Transmission = "test",
                Drive = "test",
                MeterReadout = 99999,
                Fuel = "test",
                NumberKeys = 2,
                ServiceManual = true,
                SecondTireSet = true,
                AuctionId = 1,
                PrimaryDamage = "test",
                SecondaryDamage = "test",
                VIN = "test",
                SaleTerm = "test",
                Highlights = "test",
            };

            //Act

            var bids = await vehicleService.CreateVehicleAsync(dto);

            //Assert

            Assert.NotNull(bids);
        }


        [Fact]
        public async Task DeleteVehicleAsync_WithInvalidData_ShouldReturnException()
        {
            //Arrange

            //Act

            Func<Task> act = () => vehicleService.DeleteVehicleAsync(99);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Vehicle not found.", exception.Message);
        }
        
        [Fact]
        public async Task UpdateVehicleAsync_WithValidData_ShouldReturnVehicle()
        {
            //Arrange

            var dto = new EditVehicleDto()
            {
                RegistrationYear = 2000,
                Color = "test1",
                BodyType = "test1",
                Drive = "test1",
                Transmission = "test1",
                Fuel = "test1",
                Highlights = "test1",
                SaleTerm = "test1",
                Producer = "test1",
                ModelGeneration = "test1",
                ModelSpecifer = "test1",
                PrimaryDamage = "test1",
                SecondaryDamage = "test1",
                VIN = "test1",
                Auction = 2,
            };

            //Act

            var vehicle = await vehicleService.UpdateVehicleAsync(1, dto);

            //Assert

            Assert.NotNull(vehicle);
            Assert.Equal(dto.RegistrationYear, vehicle.RegistrationYear);
            Assert.Equal(dto.Color, vehicle.Color);
            Assert.Equal(dto.BodyType, vehicle.BodyType);
            Assert.Equal(dto.Transmission, vehicle.Transmission);
            Assert.Equal(dto.Fuel, vehicle.Fuel);
            Assert.Equal(dto.Auction, vehicle.AuctionId);
        }

        [Fact]
        public async Task UpdateVehicleAsync_WithInvalidData_ShouldReturnException()
        {
            //Arrange

            var dto = new EditVehicleDto()
            {
            };

            //Act

            Func<Task> act = () => vehicleService.UpdateVehicleAsync(99, dto);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Vehicle not found.", exception.Message);
        }


        [Fact]
        public async Task BidVehicleAsync_WithValidData_ShouldReturnTrue()
        {
            //Arrange

            var dto = new UpdateBidDto()
            {
                lotNumber = 1,
                bidNow = 2000,
                userId = 1,
            };

            //Act

            var vehicle = await vehicleService.BidVehicleAsync(dto);

            //Assert

            Assert.True(vehicle);

        }


        [Fact]
        public async Task BidVehicleAsync_WithInvalidDataTooLowCurrentBid_ShouldReturnFalse()
        {
            //Arrange

            var dto = new UpdateBidDto()
            {
                lotNumber = 1,
                bidNow = 500,
                userId = 1,
            };

            //Act

            var vehicle = await vehicleService.BidVehicleAsync(dto);

            //Assert

            Assert.False(vehicle);
        }

        [Fact]
        public async Task BidVehicleAsync_WithInvalidDataLotNumber_ShouldReturnException()
        {
            //Arrange

            var dto = new UpdateBidDto()
            {
                lotNumber = 99,
                bidNow = 500,
                userId = 1,
            };

            //Act

            Func<Task> act = () => vehicleService.BidVehicleAsync(dto);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Vehicle not found.", exception.Message);
        }

        [Fact]
        public async Task BidVehicleAsync_WithInvalidDataUserId_ShouldReturnException()
        {
            //Arrange

            var dto = new UpdateBidDto()
            {
                lotNumber = 1,
                bidNow = 5000,
                userId = 99,
            };

            //Act

            Func<Task> act = () => vehicleService.BidVehicleAsync(dto);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("User not found.", exception.Message);
        }

        [Fact]
        public async Task WatchVehicleAsync_WithInvalidDataLotNumber_ShouldReturnException()
        {
            //Arrange

            var dto = new WatchDto()
            {
                VehicleId = 99,
                UserId = 1,
            };

            //Act

            Func<Task> act = () => vehicleService.WatchVehicleAsync(dto);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Vehicle not found.", exception.Message);
        }


        [Fact]
        public async Task WatchVehicleAsync_WithInvalidDataUserId_ShouldReturnException()
        {
            //Arrange

            var dto = new WatchDto()
            {
                VehicleId = 1,
                UserId = 99,
            };

            //Act

            Func<Task> act = () => vehicleService.WatchVehicleAsync(dto);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("User not found.", exception.Message);
        }


        [Fact]
        public async Task RemoveWatchAsync_WithInvalidDataLotNumber_ShouldReturnException()
        {
            //Arrange

            var dto = new WatchDto()
            {
                VehicleId = 99,
                UserId = 2,
            };

            //Act

            Func<Task> act = () => vehicleService.RemoveWatchAsync(dto);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal("Vehicle not found.", exception.Message);
        }

        
    }
}
