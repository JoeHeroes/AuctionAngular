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
            var vehicles = await _dbContext
                .Vehicles
                .ToListAsync();

            return await PictureProcess(vehicles, status);
        }



        public async Task<List<ViewVehiclesDto>> PictureProcess(List<Vehicle> vehicles, bool status)
        {
            var viewVehicle = new List<ViewVehiclesDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = _dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                var pictures = new List<string>();

                foreach (var pic in restultPictures)
                    pictures.Add(pic.PathImg);

                if (vehicle.Confirm == status)
                    viewVehicle.Add(ViewVehiclesDtoConvert(vehicle, pictures));
            }

            return viewVehicle;
        }

        public async Task<IEnumerable<ViewAdminVehiclesDto>> GetVehicleAuctionEndAsync()
        {
            var vehicles = await _dbContext
                .Vehicles
                .ToListAsync();

            var viewVehicle = new List<ViewAdminVehiclesDto>();

            foreach (var vehicle in vehicles)
            {

                var auction = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == vehicle.AuctionId && a.SalesFinised == true);

                if(auction != null)
                    viewVehicle.Add(AdminVehiclesDtoConvert(vehicle, auction));
            }

            return viewVehicle;
        }


        public async Task<IEnumerable<ViewVehiclesDto>> GetAllBidedAsync(int id)
        {

            var bids = _dbContext.Bids.Where(x => x.UserId == id);

            var vehiclesReult = await _dbContext
                .Vehicles
                .ToListAsync();

            var vehicles = new List<Vehicle>();

            var vehiclesList = _dbContext.Vehicles.ToList();

            foreach (var x in bids)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId);

                if (veh != null)
                    vehicles.Add(veh);
            }

            List<ViewVehiclesDto> viewVehicle = new List<ViewVehiclesDto>();

            await PictureProcess(vehicles, true);

            return viewVehicle;
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
                    var auc = auctionsList.FirstOrDefault(a => a.Id == veh.AuctionId && a.SalesFinised == true);

                    if (auc != null)
                        vehicles.Add(veh);
                }
            }

            return await PictureProcess(vehicles, true);
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
                    var auc = auctionsList.FirstOrDefault(a => a.Id == veh.AuctionId && a.SalesFinised == true);

                    if (auc != null)
                        vehicles.Add(veh);
                }
            }

            return await PictureProcess(vehicles, true);
        }

        public async Task<int> CreateVehicleAsync(CreateVehicleDto dto)
        {
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
                SaleTerm = dto.SaleTerm,
                Highlights = dto.Highlights,
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
            vehicle.Highlights = dto.Highlights != "" ? dto.Highlights : vehicle.Highlights;

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

            if (await _dbContext.Watches.FirstOrDefaultAsync(x => x.VehicleId == dto.VehicleId) == null)
            {
                Auction? auction = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == dto.VehicleId);

                var newEvent = new Event()
                {
                    Title = vehicle!.Id + " " + vehicle.Producer + " " + vehicle.ModelGeneration,
                    Description = "",
                    Start = auction != null ? auction.DateTime: new DateTime(),
                    End = auction != null ? auction.DateTime : new DateTime(),
                    Color = vehicle.Color,
                    AllDay = true,
                    Owner = user!.Id,
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

            if (events != null && events.Owner == dto.UserId)
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

            return await PictureProcess(vehicles, true);
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
                AllDay = false,
                Owner = 0, //For All Users
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


            if (vehicle != null)
                vehicle!.Confirm = !vehicle.Confirm;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }


        public async Task SoldVehicleAsync(int id)
        {

            var vehicle = await _dbContext
                                .Vehicles
                                .FirstOrDefaultAsync(x => x.Id == id);

            var payment = await _dbContext
                                .Payments
                                .FirstOrDefaultAsync(x => x.LotId == id);

            if(payment != null)
            {
                vehicle!.Sold = !vehicle.Sold;
                payment!.StatusSell = !payment.StatusSell;
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
                Highlights = vehicle.Highlights,
                SaleTerm = vehicle.SaleTerm,
                CurrentBid = vehicle.CurrentBid,
                WinnerId = vehicle.WinnerId,
                Images = pictures,
                DateTime = auction != null ? auction.DateTime: new DateTime(),
                Sold = vehicle.Sold,
                WaitingForConfirm = auction != null ? auction.SalesFinised : false,
            };
        }

        public ViewVehiclesDto ViewVehiclesDtoConvert(Vehicle vehicle, List<string> pictures)
        {
            var auction = _dbContext.Auctions.FirstOrDefault(a => a.Id == vehicle.AuctionId);

            return new ViewVehiclesDto()
            {
                LotNumber = vehicle.Id,
                Image = pictures.Count() == 0 ? "" : pictures.First(),
                Producer = vehicle.Producer,
                ModelSpecifer = vehicle.ModelSpecifer,
                ModelGeneration = vehicle.ModelGeneration,
                RegistrationYear = vehicle.RegistrationYear,
                MeterReadout = vehicle.MeterReadout,
                DateTime = auction != null ? auction!.DateTime : new DateTime(),
                CurrentBid = vehicle.CurrentBid,
                Sold = vehicle.Sold,
                Confirm = vehicle.Confirm,
            };
        }


        public ViewAdminVehiclesDto AdminVehiclesDtoConvert(Vehicle vehicle, Auction auction)
        {
            return new ViewAdminVehiclesDto()
            {
                LotNumber = vehicle.Id,
                Producer = vehicle.Producer,
                ModelSpecifer = vehicle.ModelSpecifer,
                ModelGeneration = vehicle.ModelGeneration,
                RegistrationYear = vehicle.RegistrationYear,
                MeterReadout = vehicle.MeterReadout,
                DateTime = auction!.DateTime,
                CurrentBid = vehicle.CurrentBid,
                Sold = vehicle.Sold,
                Confirm = vehicle.Confirm
            };
        }
    }
}