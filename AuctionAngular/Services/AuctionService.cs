﻿using AuctionAngular.Dtos;
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
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now && x.SalesFinised == false);

            auction.SalesStarted = true;

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
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.SalesStarted == true && x.SalesFinised == false);

            if (auction.DateTime.AddMinutes(1) <= DateTime.Now)
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

        public async Task<IEnumerable<ViewVehicleDto>> LiveAuctionListAsync()
        {
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now && x.SalesFinised == false && x.SalesStarted == false);

            List<ViewVehicleDto> result = new List<ViewVehicleDto>();

            var vehicles = await _dbContext.Vehicles.Where(x => x.AuctionId == auction.Id).ToListAsync();

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

        public async Task<IEnumerable<Auction>> AuctionListAsync()
        {
            var auctions = await _dbContext.Auctions.Where(x => x.SalesFinised == false).ToListAsync();

            return auctions;
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
                AuctionId = vehicle.AuctionId,
                PrimaryDamage = vehicle.PrimaryDamage,
                SecondaryDamage = vehicle.SecondaryDamage,
                VIN = vehicle.VIN,
                Highlights = vehicle.Highlights,
                CurrentBid = vehicle.CurrentBid,
                WinnerId = vehicle.WinnerId,
                Images = pictures,
            };
        }
    }
}
