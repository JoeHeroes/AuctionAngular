using AuctionAngular.Dtos.Auction;
using AuctionAngular.Dtos.Bid;
using AuctionAngular.Dtos.Vehicle;
using AuctionAngular.Dtos.Watch;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IWebHostEnvironment _webHost;
        /// <inheritdoc/>
        public VehicleService(AuctionDbContext dbContext, IWebHostEnvironment webHost)
        {
            _dbContext = dbContext;
            _webHost = webHost;
        }

        public async Task<ViewVehicleDto> GetByIdVehicleAsync(int id)
        {
            var vehicle = await _dbContext
                .Vehicles
                .FirstOrDefaultAsync(u => u.Id == id);

            if(vehicle is null)
                throw new NotFoundException("Vehicle not found.");

            var restultPictures = _dbContext.Pictures.Where(x => x.VehicleId == vehicle!.Id);

            List<string> pictures = new List<string>();

            foreach (var pic in restultPictures)
                pictures.Add(pic.PathImg);

            return ViewVehicleDtoConvert(vehicle!, pictures);
        }

        public async Task<IEnumerable<ViewVehiclesDto>> GetVehiclesAsync(bool status)
        {
            List<Vehicle> vehicles = await _dbContext.Vehicles.Where(x => x.isSold == false).ToListAsync();

            return await AddPicture(vehicles, status);
        }



        public async Task<List<ViewVehiclesDto>> AddPicture(List<Vehicle> vehicles, bool status)
        {
            var viewVehicle = new List<ViewVehiclesDto>();

            var picturesBase = _dbContext.Pictures.ToList();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = picturesBase.Where(x => x.VehicleId == vehicle.Id);

                var pictures = new List<string>();

                foreach (var pic in restultPictures)
                    pictures.Add(pic.PathImg);

                if (vehicle.isConfirm == status)
                    viewVehicle.Add(ViewVehiclesDtoConvert(vehicle, pictures));
            }

            return viewVehicle;
        }

        public async Task<IEnumerable<ViewAdminVehiclesDto>> GetVehicleAuctionEndAsync()
        {
            var vehicles = await _dbContext
                .Vehicles
                .ToListAsync();

            var payments = _dbContext
               .Payments;

            var viewVehicle = new List<ViewAdminVehiclesDto>();

            foreach (var vehicle in vehicles)
            {
                var auction = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == vehicle.AuctionId && a.isFinised == true);

                var payment = payments.FirstOrDefault(x => x.LotId == vehicle.Id);

                if (auction != null && payment == null)
                    viewVehicle.Add(AdminVehiclesDtoConvert(vehicle, auction));
            }

            return viewVehicle;
        }


        public async Task<IEnumerable<ViewVehiclesDto>> GetAllBidedAsync(int id)
        {

            var bids = _dbContext.Bids.Where(x => x.UserId == id);

            if (bids is null)
                throw new NotFoundException("Bids not found.");

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = _dbContext.Vehicles.ToList();

            foreach (var x in bids)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId);

                if (veh != null)
                    vehicles.Add(veh);
            }

            return await AddPicture(vehicles, true);
        }

        public async Task<IEnumerable<ViewVehiclesDto>> GetAllWonAsync(int id)
        {

            var bids = _dbContext.Bids.Where(x => x.UserId == id);

            if (bids is null)
                throw new NotFoundException("Bids not found.");

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = _dbContext.Vehicles.ToList();

            var auctionsList = _dbContext.Auctions.ToList();

            foreach (var x in bids)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId && d.WinnerId == id);

                if(veh != null)
                {
                    var auc = auctionsList.FirstOrDefault(a => a.Id == veh.AuctionId && a.isFinised == true);

                    if (auc != null)
                        vehicles.Add(veh);
                }
            }

            return await AddPicture(vehicles, true);
        }

        public async Task<IEnumerable<ViewVehiclesDto>> GetAllLostAsync(int id)
        {

            var bids = _dbContext.Bids.Where(x => x.UserId == id);

            if (bids is null)
                throw new NotFoundException("Bids not found.");

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = _dbContext.Vehicles.ToList();

            var auctionsList = _dbContext.Auctions.ToList();

            foreach (var x in bids)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId && d.WinnerId != id);
                if(veh != null)
                {
                    var auc = auctionsList.FirstOrDefault(a => a.Id == veh.AuctionId && a.isFinised == true);

                    if (auc != null)
                        vehicles.Add(veh);
                }
            }

            return await AddPicture(vehicles, true);
        }

        public async Task<int> CreateVehicleAsync(CreateVehicleDto dto)
        {

            if(dto.EngineCapacity < 0)
            {
                throw new Exception();
            }

            if (dto.EngineOutput < 0)
            {
                throw new Exception();
            }

            if (dto.MeterReadout < 0)
            {
                throw new Exception();
            }

            if (dto.NumberKeys < 0)
            {
                throw new Exception();
            }

            var vehicle = new Vehicle
            {
                Producer = dto.Producer,
                ModelSpecifer = dto.ModelSpecifer,
                ModelGeneration = dto.ModelGeneration,
                RegistrationYear = dto.RegistrationYear,
                Color = dto.Color,
                BodyType = dto.BodyType,
                EngineCapacity = dto.EngineCapacity,
                EngineOutput = dto.EngineOutput,
                Transmission = dto.Transmission,
                Drive = dto.Drive,
                MeterReadout = dto.MeterReadout,
                Fuel = dto.Fuel,
                NumberKeys = dto.NumberKeys,
                ServiceManual = dto.ServiceManual,
                SecondTireSet = dto.SecondTireSet,
                CurrentBid = 0,
                PrimaryDamage = dto.PrimaryDamage,
                SecondaryDamage = dto.SecondaryDamage,
                VIN = dto.VIN,
                AuctionId = dto.AuctionId,
                OwnerId = dto.OwnerId,
                SaleTerm = dto.SaleTerm,
                Category = dto.Category,
            };


            await _dbContext.Vehicles.AddAsync(vehicle);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            //await AddEventVehicleAsync(vehicle.Id, dto);

            return vehicle.Id;
        }
        public async Task DeleteVehicleAsync(int id)
        {
            var result = _dbContext
                .Vehicles
                .FirstOrDefault(u => u.Id == id);

            if (result is null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            _dbContext.Vehicles.Remove(result);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task<Vehicle> UpdateVehicleAsync(int id, EditVehicleDto dto)
        {
            var vehicle = await _dbContext
                .Vehicles
                .FirstOrDefaultAsync(u => u.Id == id);

            if (vehicle is null)
                throw new NotFoundException("Vehicle not found.");

            vehicle.Producer = dto.Producer != "" ? dto.Producer : vehicle.Producer;
            vehicle.ModelSpecifer = dto.ModelSpecifer != "" ? dto.ModelSpecifer : vehicle.ModelSpecifer;
            vehicle.ModelGeneration = dto.ModelGeneration != "" ? dto.ModelGeneration : vehicle.ModelGeneration;
            vehicle.RegistrationYear = dto.RegistrationYear.ToString() != "" ? dto.RegistrationYear : vehicle.RegistrationYear;
            vehicle.Color = dto.Color != "" ? dto.Color : vehicle.Color;
            vehicle.AuctionId = dto.Auction.ToString() != "" ? dto.Auction : vehicle.AuctionId;
            vehicle.BodyType = dto.BodyType != "" ? dto.BodyType : vehicle.BodyType;
            vehicle.Transmission = dto.Transmission != "" ? dto.Transmission : vehicle.Transmission;
            vehicle.Drive = dto.Drive != "" ? dto.Drive : vehicle.Drive;
            vehicle.MeterReadout = dto.MeterReadout.ToString() != "" ? dto.MeterReadout : vehicle.MeterReadout;
            vehicle.Fuel = dto.Fuel != "" ? dto.Fuel : vehicle.Fuel;
            vehicle.PrimaryDamage = dto.PrimaryDamage != "" ? dto.PrimaryDamage : vehicle.PrimaryDamage;
            vehicle.SecondaryDamage = dto.SecondaryDamage != "" ? dto.SecondaryDamage : vehicle.SecondaryDamage;
            vehicle.EngineCapacity = dto.EngineCapacity.ToString() != "" ? dto.EngineCapacity : vehicle.EngineCapacity;
            vehicle.EngineOutput = dto.EngineOutput.ToString() != "" ? dto.EngineOutput : vehicle.EngineOutput;
            vehicle.NumberKeys = dto.NumberKeys.ToString() != "" ? dto.NumberKeys : vehicle.NumberKeys;
            vehicle.ServiceManual = dto.ServiceManual.ToString() != "" ? dto.ServiceManual : vehicle.ServiceManual;
            vehicle.SecondTireSet = dto.SecondTireSet.ToString() != "" ? dto.SecondTireSet : vehicle.SecondTireSet;
            vehicle.VIN = dto.VIN != "" ? dto.VIN : vehicle.VIN;
            vehicle.SaleTerm = dto.SaleTerm != "" ? dto.SaleTerm : vehicle.SaleTerm;
            vehicle.Category = dto.Category != "" ? dto.Category : vehicle.Category;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return vehicle;
        }

        public async Task<bool> BidVehicleAsync(UpdateBidDto dto)
        {
            var vehicle = await _dbContext
                                .Vehicles
                                .FirstOrDefaultAsync(x => x.Id == dto.lotNumber);

            if (vehicle is null)
                throw new NotFoundException("Vehicle not found.");

            if (dto.bidNow > vehicle?.CurrentBid)
            {
                vehicle.WinnerId = dto.userId;
                vehicle.CurrentBid = dto.bidNow;

                User? user = await _dbContext
                                  .Users
                                  .FirstOrDefaultAsync(x => x.Id == dto.userId);

                if (user is null)
                    throw new NotFoundException("User not found.");

                var bind = new Bid()
                {
                    UserId = user!.Id,
                    VehicleId = vehicle.Id,
                };
                
                if (await _dbContext.Bids.FirstOrDefaultAsync(x => x.UserId == user.Id && x.VehicleId == vehicle.Id) == null)
                    await _dbContext.Bids.AddAsync(bind);

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                return true;
            }
            return false;
        }

        public async Task WatchVehicleAsync(WatchDto dto)
        {
            var user = await _dbContext
                                    .Users
                                    .FirstOrDefaultAsync(x => x.Id == dto.UserId);

            if (user is null)
                throw new NotFoundException("User not found.");

            var vehicle = await _dbContext
                                    .Vehicles
                                    .FirstOrDefaultAsync(x => x.Id == dto.VehicleId);

            if (vehicle is null)
                throw new NotFoundException("Vehicle not found.");

            var observed = new Watch()
            {
                UserMany = user!,
                VehicleMany = vehicle!,
            };

            if (await _dbContext.Watches.FirstOrDefaultAsync(x => x.VehicleId == dto.VehicleId && x.UserId == user.Id) == null)
            {
                Auction? auction = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == vehicle.AuctionId);

                var newEvent = new Event()
                {
                    Title = vehicle!.Id + " " + vehicle.Producer + " " + vehicle.ModelGeneration,
                    Description = "",
                    Start = auction != null ? auction.DateTime: new DateTime(),
                    End = auction != null ? auction.DateTime : new DateTime(),
                    Color = vehicle.Color,
                    isAllDay = true,
                    UserId = user!.Id,
                    Url = "/vehicle/lot/"+ vehicle.Id
                };

                await _dbContext.Events.AddAsync(newEvent);
                await _dbContext.Watches.AddAsync(observed);
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

        public async Task RemoveWatchAsync(WatchDto dto)
        {

            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == dto.VehicleId);

            if (vehicle is null)
                throw new NotFoundException("Vehicle not found.");

            var observed = await _dbContext.Watches.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.VehicleId == vehicle!.Id);

            if (observed is null)
                throw new NotFoundException("Observed not found.");

            var events = await _dbContext.Events.FirstOrDefaultAsync(x => x.Title == vehicle!.Id + " " + vehicle.Producer + " " + vehicle.ModelGeneration);

            if (events != null && events.UserId == dto.UserId)
                _dbContext.Events.Remove(events);

            _dbContext.Watches.Remove(observed!);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task<bool> CheckWatchAsync(WatchDto dto)
        {
            var result = await _dbContext.Watches.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.VehicleId == dto.VehicleId);

            if (result == null)
                return false;
            else
                return true;
        }
        public async Task<IEnumerable<ViewVehiclesDto>> GetAllWatchAsync(int id)
        {
            var watches = await _dbContext.Watches.Where(x => x.UserId == id).ToListAsync();

            var vehicles = new List<Vehicle>();

            foreach(var watch in watches)
                vehicles.Add(await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == watch.VehicleId));

            return await AddPicture(vehicles, true);
        }

        public async Task<List<string>> AddPictureAsync(int id, IFormFileCollection files)
        {
            List<string> listPicture = new List<string>();
            foreach (var file in files)
            {
                string fileName = "";
                if (file != null)
                {
                    string uploadDir = Path.Combine(_webHost.WebRootPath, "Images");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    listPicture.Add(fileName);
                    var picture = new Picture { PathImg = fileName, VehicleId = id};
                    _dbContext.Pictures.Add(picture);
                }
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return listPicture;
        }

        public async Task AddEventVehicleAsync(int id, CreateVehicleDto dto)
        {
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.Id == dto.AuctionId);

            var eventSell = new Event()
            {
                Title = id + " " + dto.Producer + " " + dto.ModelSpecifer + " " + dto.ModelGeneration,
                Description = "",
                Start = auction!.DateTime,
                End = auction.DateTime,
                Color = dto.Color,
                isAllDay = false,
                UserId = 0, //For All Users
                Url = "/vehicle/lot"+id
            };

            await _dbContext.Events.AddAsync(eventSell);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }


        public async Task ConfirmVehicleAsync(int id)
        {

            var vehicle = await _dbContext
                                .Vehicles
                                .FirstOrDefaultAsync(x => x.Id == id);


            if (vehicle != null && vehicle.AuctionId != 0)
            {
                var auction = await _dbContext.Auctions.FirstOrDefaultAsync(x => x.Id == vehicle.AuctionId);
                if(auction!.DateTime > DateTime.Now)
                {
                    vehicle!.isConfirm = !vehicle.isConfirm;
                }
                else
                    throw new Exception();

            }
            else
                throw new Exception();

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }


        public async Task SellVehicleAsync(int id)
        {

            var vehicle = await _dbContext
                                .Vehicles
                                .FirstOrDefaultAsync(x => x.Id == id);

            //var payment = await _dbContext
            //                    .Payments
            //                    .FirstOrDefaultAsync(x => x.LotId == id);


            if (vehicle != null)
            {
                vehicle!.isSold = !vehicle.isSold;
            }
            else
                throw new Exception();

            //if (payment != null)
            //{
            //    vehicle!.isSold = !vehicle.isSold;
            //    payment!.isSold = !payment.isSold;
            //}
            //else
            //    throw new Exception();

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task RejectVehicleAsync(int id)
        {

            var vehicle = await _dbContext
                                .Vehicles
                                .FirstOrDefaultAsync(x => x.Id == id);

            //var payment = await _dbContext
            //                    .Payments
            //                    .FirstOrDefaultAsync(x => x.LotId == id);


            var auction = await _dbContext
                                .Auctions
                                .OrderByDescending(a => a.Id) .FirstAsync();

            vehicle.AuctionId = auction.Id;

            //if (payment != null)
            //{
            //    vehicle!.isSold = !vehicle.isSold;
            //    payment!.isSold = !payment.isSold;
            //}
            //else
            //    throw new Exception();

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task SetAuctionForVehicleAsync(SetAuctionDto dto)
        {

            var vehicle = await _dbContext
                                .Vehicles
                                .FirstOrDefaultAsync(x => x.Id == dto.userId);

            if (vehicle != null)
            {
                vehicle!.AuctionId = dto.auctionId;
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

        public ViewVehicleDto ViewVehicleDtoConvert(Vehicle vehicle, List<string> pictures)
        {
            var auction = _dbContext.Auctions.FirstOrDefault(x => x.Id == vehicle.AuctionId);

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
                Category = vehicle.Category,
                SaleTerm = vehicle.SaleTerm,
                CurrentBid = vehicle.CurrentBid,
                WinnerId = vehicle.WinnerId,
                OwnerId = vehicle.OwnerId,
                Images = pictures,
                DateTime = auction != null ? auction.DateTime: new DateTime(),
                isSold = vehicle.isSold,
                WaitingForConfirm = auction != null ? auction.isFinised : false,
            };
        }

        public ViewVehiclesDto ViewVehiclesDtoConvert(Vehicle vehicle, List<string> pictures)
        {
            var auction = _dbContext.Auctions.FirstOrDefault(a => a.Id == vehicle.AuctionId);

            return new ViewVehiclesDto()
            {
                LotNumber = vehicle.Id,
                AuctionNumber = vehicle.AuctionId,
                Image = pictures.Count() == 0 ? "" : pictures.First(),
                Producer = vehicle.Producer,
                ModelSpecifer = vehicle.ModelSpecifer,
                ModelGeneration = vehicle.ModelGeneration,
                RegistrationYear = vehicle.RegistrationYear,
                MeterReadout = vehicle.MeterReadout,
                DateTime = auction != null ? auction!.DateTime : new DateTime(),
                CurrentBid = vehicle.CurrentBid,
                isSold = vehicle.isSold,
                isConfirm = vehicle.isConfirm,
            };
        }


        public ViewAdminVehiclesDto AdminVehiclesDtoConvert(Vehicle vehicle, Auction auction)
        {
            return new ViewAdminVehiclesDto()
            {
                LotNumber = vehicle.Id,
                AuctionNumber = vehicle.AuctionId,
                Producer = vehicle.Producer,
                ModelSpecifer = vehicle.ModelSpecifer,
                ModelGeneration = vehicle.ModelGeneration,
                RegistrationYear = vehicle.RegistrationYear,
                MeterReadout = vehicle.MeterReadout,
                DateTime = auction!.DateTime,
                CurrentBid = vehicle.CurrentBid,
                isSold = vehicle.isSold,
                isConfirm = vehicle.isConfirm
            };
        }
    }
}