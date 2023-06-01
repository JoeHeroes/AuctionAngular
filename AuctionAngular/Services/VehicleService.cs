using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly AuctionDbContext dbContext;
        private readonly IWebHostEnvironment webHost;
        /// <inheritdoc/>
        public VehicleService(AuctionDbContext dbContext, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }

        public async Task<ViewVehicleDto> GetById(int id)
        {
            var vehicle = await this.dbContext
                .Vehicles
                .FirstOrDefaultAsync(u => u.Id == id);


            var restultPictures = this.dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

            List<string> pictures = new List<string>();

            foreach (var pic in restultPictures)
            {
                pictures.Add(pic.PathImg);
            }
            
            return ViewVehicleDtoConvert(vehicle, pictures);
        }

        public async Task<IEnumerable<ViewVehiclesDto>> GetAll()
        {
            var vehicles = await this.dbContext
                .Vehicles
                .ToListAsync();

            List<ViewVehiclesDto> viewVehicle = new List<ViewVehiclesDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = this.dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                viewVehicle.Add(ViewVehiclesDtoConvert(vehicle, pictures));

            }

            return viewVehicle;
        }


        public async Task<IEnumerable<ViewVehiclesDto>> GetAllBided(int id)
        {

            var bids = this.dbContext.Bids.Where(x => x.UserId == id);

            var vehiclesReult = await this.dbContext
                .Vehicles
                .ToListAsync();

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = this.dbContext.Vehicles.ToList();

            foreach (var x in bids)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId);

                if (veh != null)
                {
                    vehicles.Add(veh);
                }
            }

            List<ViewVehiclesDto> viewVehicle = new List<ViewVehiclesDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = this.dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                viewVehicle.Add(ViewVehiclesDtoConvert(vehicle, pictures));
            }

            return viewVehicle;
        }

        public async Task<IEnumerable<ViewVehiclesDto>> GetAllWon(int id)
        {

            var bids = this.dbContext.Bids.Where(x => x.UserId == id);

            var vehiclesReult = await this.dbContext
                .Vehicles
                .ToListAsync();

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = this.dbContext.Vehicles.ToList();

            foreach (var x in bids)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId && d.WinnerId == id && d.SalesFinised==true);

                if (veh != null)
                {
                    vehicles.Add(veh);
                }
            }

            List<ViewVehiclesDto> viewVehicle = new List<ViewVehiclesDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = this.dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                viewVehicle.Add(ViewVehiclesDtoConvert(vehicle, pictures));
            }

            return viewVehicle;
        }

        public async Task<IEnumerable<ViewVehiclesDto>> GetAllLost(int id)
        {

            var bids = this.dbContext.Bids.Where(x => x.UserId == id);

            var vehiclesReult = await this.dbContext
                .Vehicles
                .ToListAsync();

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = this.dbContext.Vehicles.ToList();

            foreach (var x in bids)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId && d.WinnerId != id && d.SalesFinised == true);

                if (veh != null)
                {
                    vehicles.Add(veh);
                }
            }

            List<ViewVehiclesDto> viewVehicle = new List<ViewVehiclesDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = this.dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                viewVehicle.Add(ViewVehiclesDtoConvert(vehicle, pictures));
            }

            return viewVehicle;
        }

        public async Task<int> Create(CreateVehicleDto dto)
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
                DateTime = dto.DateTime,
                VIN = dto.VIN,
                LocationId = dto.LocationId,
                SalesFinised = false,
                SaleTerm = dto.SaleTerm,
                Highlights = dto.Highlights,
            };


            this.dbContext.Vehicles.Add(vehicle);
            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            AddEvent(vehicle.Id, dto);

            return vehicle.Id;
        }
        public async Task Delete(int id)
        {
            var result = this.dbContext
                .Vehicles
                .FirstOrDefault(u => u.Id == id);

            if (result is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            this.dbContext.Vehicles.Remove(result);
            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task Update(int id, EditVehicleDto dto)
        {
            var vehicle = await this.dbContext
                .Vehicles
                .FirstOrDefaultAsync(u => u.Id == id);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            vehicle.RegistrationYear = dto.RegistrationYear;
            vehicle.Color = dto.Color;
            vehicle.BodyType = dto.BodyType;
            vehicle.Transmission = dto.Transmission;
            vehicle.LocationId = dto.LocationId;
            vehicle.Fuel = dto.Fuel;
            vehicle.DateTime = dto.DateTime;

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task Bid(UpdateBidDto dto)
        {
            Vehicle vehicle = await this.dbContext
                                .Vehicles
                                .FirstOrDefaultAsync(x => x.Id == dto.lotNumber);

            if (dto.bidNow > vehicle.CurrentBid)
            {
                vehicle.WinnerId = dto.userId;
                vehicle.CurrentBid = dto.bidNow;

                User user = await this.dbContext
                                  .Users
                                  .FirstOrDefaultAsync(x => x.Id == dto.userId);

                var bind = new Bid()
                {
                    UserId = user.Id,
                    VehicleId = vehicle.Id,
                };

                /*
                if (this.dbContext.Bids.FirstOrDefault(x => x.UserId == user.Id && x.VehicleId == vehicle.Id) == null)
                {
                    this.dbContext.Bids.Add(bind);
                }
                */
                this.dbContext.Bids.Add(bind);

                try
                {
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
            }
        }

        public async Task Watch(WatchDto dto)
        {
            User user = await this.dbContext
                                    .Users
                                    .FirstOrDefaultAsync(x => x.Id == dto.UserId);

            Vehicle vehicle = await this.dbContext
                                    .Vehicles
                                    .FirstOrDefaultAsync(x => x.Id == dto.VehicleId);

            var observed = new Watch()
            {
                UserMany = user,
                VehicleMany = vehicle,
            };

            if (await this.dbContext.Watches.FirstOrDefaultAsync(x => x.VehicleId == dto.VehicleId) == null)
            {
                var newEvent = new Event()
                {
                    Title = vehicle.Id + " " + vehicle.Producer + " " + vehicle.ModelGeneration,
                    Start = vehicle.DateTime,
                    End = vehicle.DateTime,
                    Color = vehicle.Color,
                    AllDay = true,
                    Owner = user.Id,
                };

                this.dbContext.Events.Add(newEvent);
                this.dbContext.Watches.Add(observed);
            }

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

        }

        public async Task RemoveWatch(WatchDto dto)
        {

            Vehicle vehicle = await this.dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == dto.VehicleId);

            Watch observed = await this.dbContext.Watches.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.VehicleId == vehicle.Id);


            var events = await this.dbContext.Events.FirstOrDefaultAsync(x => x.Title == vehicle.Id + " " + vehicle.Producer + " " + vehicle.ModelGeneration);

            if (events != null && events.Owner == dto.UserId)
            {
                this.dbContext.Events.Remove(events);
            }

            this.dbContext.Watches.Remove(observed);

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public async Task<bool> CheckWatch(WatchDto dto)
        {
            var result = await this.dbContext.Watches.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.VehicleId == dto.VehicleId);

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<ViewVehiclesDto>> GetAllWatch(int id)
        {
            var watches = await this.dbContext.Watches.Where(x => x.UserId == id).ToListAsync();

            var vehicles = new List<Vehicle>();

            foreach(var watch in watches)
            {
                var result = await this.dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == watch.VehicleId);
                vehicles.Add(result);
            }

            List<ViewVehiclesDto> viewVehicle = new List<ViewVehiclesDto>();

            foreach (var vehicle in vehicles)
            {
                var restultPictures = this.dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }

                viewVehicle.Add(ViewVehiclesDtoConvert(vehicle, pictures));
            }

            return viewVehicle;
        }

        public async Task<List<string>> AddPicture(int id, IFormFileCollection files)
        {
            List<string> listPicture = new List<string>();
            foreach (var file in files)
            {
                string fileName = "";
                if (file != null)
                {
                    string uploadDir = Path.Combine(webHost.WebRootPath, "Images");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    listPicture.Add(fileName);
                    var picture = new Picture { PathImg = fileName, VehicleId = id};
                    this.dbContext.Pictures.Add(picture);
                }
            }

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return listPicture;
        }

        public async Task AddEvent(int id, CreateVehicleDto dto)
        {
            var eventSell = new Event()
            {
                Title = id + " " + dto.Producer + " " + dto.ModelSpecifer + " " + dto.ModelGeneration,
                Start = dto.DateTime,
                End = dto.DateTime,
                Color = dto.Color,
                AllDay = false,
                Owner = 0 //For All Users
            };

            this.dbContext.Events.Add(eventSell);

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

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

        public ViewVehiclesDto ViewVehiclesDtoConvert(Vehicle vehicle, List<string> pictures)
        {
            return new ViewVehiclesDto()
            {
                LotNumber = vehicle.Id,
                Image = pictures.Count() == 0 ? "" : pictures.First(),
                Producer = vehicle.Producer,
                ModelSpecifer = vehicle.ModelSpecifer,
                ModelGeneration = vehicle.ModelGeneration,
                RegistrationYear = vehicle.RegistrationYear,
                MeterReadout = vehicle.MeterReadout,
                DateTime = vehicle.DateTime,
                CurrentBid = vehicle.CurrentBid,
            };
        }

    }
}