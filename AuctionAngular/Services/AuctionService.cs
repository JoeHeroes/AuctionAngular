using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly AuctionDbContext _dbContext;
        /// <inheritdoc/>
        public AuctionService(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> LiveAuction()
        {
            var result = await _dbContext.Vehicles.Where(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now && x.SalesFinised == false).AnyAsync();

            return result;
        }

        public async Task EndAuction()
        {
            var result = await _dbContext.Vehicles.Where(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now && x.SalesFinised == false).ToListAsync();

            foreach (var res in result)
            {
                res.SalesFinised = true;
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task<IEnumerable<ViewVehicleDto>> LiveAuctionList()
        {
            var vehicles = await _dbContext.Vehicles.Where(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now).ToListAsync();

            List<ViewVehicleDto> result = new List<ViewVehicleDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = _dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                ViewVehicleDto view = ViewVehicleDtoConvert(vehicle, pictures);

                result.Add(view);

            }

            return result;
        }

        public async Task<IEnumerable<ViewVehicleDto>> AuctionList()
        {
            var vehicles = await _dbContext.Vehicles.ToListAsync();

            List<ViewVehicleDto> result = new List<ViewVehicleDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = _dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                ViewVehicleDto view = ViewVehicleDtoConvert(vehicle, pictures);

                result.Add(view);
            }

            return result;
        }


        public ViewVehicleDto ViewVehicleDtoConvert(Vehicle vehicle, List<string> pictures)
        {

            return new ViewVehicleDto()
            {
                Id = vehicle.Id,
                Producer = vehicle.Producer,
                ModelSpecifer = vehicle.ModelSpecifer,
                ModelGeneration = vehicle.ModelGeneration,
                RegistrationYear = vehicle.RegistrationYear,
                Color = vehicle.Color,
                BodyType = vehicle.BodyType,
                EngineCapacity = vehicle.EngineCapacity,
                EngineOutput = vehicle.EngineOutput,
                Transmission = vehicle.Transmission,
                Drive = vehicle.Drive,
                MeterReadout = vehicle.MeterReadout,
                Fuel = vehicle.Fuel,
                NumberKeys = vehicle.NumberKeys,
                ServiceManual = vehicle.ServiceManual,
                SecondTireSet = vehicle.SecondTireSet,
                LocationId = vehicle.LocationId,
                PrimaryDamage = vehicle.PrimaryDamage,
                SecondaryDamage = vehicle.SecondaryDamage,
                VIN = vehicle.VIN,
                Highlights = vehicle.Highlights,
                DateTime = vehicle.DateTime,
                CurrentBid = vehicle.CurrentBid,
                WinnerId = vehicle.WinnerId,
                SalesFinised = vehicle.SalesFinised,
                Images = pictures,
            };
        }


        public IEnumerable<ViewVehicleDto> AuctionListSpecial()
        {
            var vehicles = _dbContext.Vehicles.ToList();

            List<ViewVehicleDto> result = new List<ViewVehicleDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = _dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                ViewVehicleDto view = ViewVehicleDtoConvert(vehicle, pictures);

                result.Add(view);
            }

            return result;
        }
    }
}
