using AuctionAngular.Dtos.Auction;
using AuctionAngular.Dtos.Vehicle;
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

        public async Task<bool> LiveAuctionAsync()
        {
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now && x.SalesFinised == false);

            return auction != null ? true : false;
        }

        public async Task<bool> StartedAuctionAsync()
        {
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now && x.SalesStarted == true);

            return auction != null ? true : false;
        }

        public async Task StartAuctionAsync()
        {
            await Console.Out.WriteLineAsync("Auction Start!!!");

            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now && x.SalesFinised == false);

            auction!.SalesStarted = true;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            //await Task.Delay(TimeSpan.FromHours(1));
            await Task.Delay(TimeSpan.FromMinutes(1));

            await EndAuctionAsync();
        }

        public async Task EndAuctionAsync()
        {
            await Console.Out.WriteLineAsync("Auction End!!!");

            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.SalesStarted == true && x.SalesFinised == false);

            if (auction != null && auction!.DateTime.AddMinutes(1) <= DateTime.Now)
            {
                auction.SalesFinised = true;
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

        public async Task CreateAuctionAsync(CreateAuctionDto dto)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Name == dto.Location);

            if (dto.AuctionDate < DateTime.Now)
                throw new Exception();

            var auction = new Auction()
            {
                DateTime = dto.AuctionDate.AddHours(12),
                LocationId = location != null ? location.Id : 0,
                Description = dto.Description,
                SalesStarted = false,
                SalesFinised = false
            };

            _dbContext.Auctions.Add(auction);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

        }

        public async Task DeleteAuctionAsync(int id)
        {
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.Id == id);

            if (auction is null)
                throw new NotFoundException("Auction not found.");

            _dbContext.Auctions.Remove(auction);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task EditAuctionAsync(EditAuctionDto dto)
        {

            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (auction is null)
                throw new NotFoundException("Auction not found.");

            if(dto.Location != "")
            {
                var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Name == dto.Location);

                if (location == null)
                    auction.LocationId = 0;
                else
                    auction.LocationId = location.Id;
            }
         



            auction.Description = dto.Description != "" ? dto.Description : auction.Description;
            auction.DateTime = dto.AuctionDate.ToString() != default(DateTime).ToString() ? dto.AuctionDate : auction.DateTime;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task<ViewAuctionDto> GetByIdAuctionAsync(int id)
        {
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.Id == id);

            return await ViewAuctionDtoConvert(auction);
        }

        public async Task<IEnumerable<ViewVehicleDto>> LiveAuctionListAsync()
        {
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now && x.SalesFinised == false && x.SalesStarted == true);

            var result = new List<ViewVehicleDto>();

            if(auction == null)
            {
                return result;
            }

            var vehicles = await _dbContext.Vehicles.Where(x => x.AuctionId == auction!.Id).ToListAsync();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = _dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                var pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                ViewVehicleDto view = await ViewVehicleDtoConvert(vehicle, pictures);

                result.Add(view);
            }

            return result;
        }

        public async Task<IEnumerable<ViewAuctionDto>> AuctionListAsync()
        {
            var auctions = await _dbContext.Auctions.Where(x => x.SalesFinised == false).ToListAsync();

            var auctionDto = new List<ViewAuctionDto>();

            foreach (var auc in auctions)
            {
                if (auc.DateTime > DateTime.Now)
                {
                    auctionDto.Add(await ViewAuctionDtoConvert(auc));
                }
            }

            return auctionDto;
        }


        public async Task<ViewVehicleDto> ViewVehicleDtoConvert(Vehicle vehicle, List<string> pictures)
        {

            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.Id == vehicle.AuctionId);
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
                AuctionId = vehicle.AuctionId,
                PrimaryDamage = vehicle.PrimaryDamage,
                SecondaryDamage = vehicle.SecondaryDamage,
                VIN = vehicle.VIN,
                Highlights = vehicle.Highlights,
                DateTime = auction!.DateTime,
                CurrentBid = vehicle.CurrentBid,
                WinnerId = vehicle.WinnerId,
                Images = pictures,
                isSold = vehicle.isSold,
                WaitingForConfirm = auction.SalesFinised
            };
        }

        public async Task<ViewAuctionDto> ViewAuctionDtoConvert(Auction auction)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == auction.LocationId);

            var vehicles = await _dbContext.Vehicles.Where(x => x.AuctionId == auction.Id).ToListAsync();

            return new ViewAuctionDto()
            {
                Id = auction.Id,
                DateTime = auction.DateTime,
                Description = auction.Description,
                CountVehicle = vehicles.Count(),
                Location = location != null ? location!.Name: "Unknown",
            };
        }
    }
}
