
using AuctionAngular.DTO;
using AuctionAngular.Enum;
using AuctionAngular.Models;
using AuctionAngular.Models.DTO;
using AuctionAngular.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
  
    public class VehicleService : IVehicleService
    {
        private readonly AuctionDbContext dbContext;
        private readonly IWebHostEnvironment webHost;
        public VehicleService(AuctionDbContext dbContext, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }

        private string UploadFile(CreateVehicleDto dto)
        {
            string fileName = null;
            if (dto.PathPic != null)
            {
                string uploadDir = Path.Combine(webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + dto.PathPic.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dto.PathPic.CopyTo(fileStream);
                }
            }
            return fileName;
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


            ViewVehicleDto view = new ViewVehicleDto()
            {
                Id = vehicle.Id,
                Producer = vehicle.Producer,
                ModelSpecifer = vehicle.ModelSpecifer,
                ModelGeneration = vehicle.ModelGeneration,
                RegistrationYear = vehicle.RegistrationYear,
                Color = vehicle.Color,
                BodyType = vehicle.BodyType,
                EngineCapacity= vehicle.EngineCapacity,
                EngineOutput = vehicle.EngineOutput,
                Transmission = vehicle.Transmission,
                Drive= vehicle.Drive,
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
                SalesFinised= vehicle.SalesFinised,
                Images = pictures,
            };

            return view;
        }

        public async Task<IEnumerable<ViewVehiclesDto>> GetAll()
        {
            var vehicles = await this.dbContext
                .Vehicles
                .ToListAsync();

            List<ViewVehiclesDto> viewVehicle = new List<ViewVehiclesDto>();

           foreach (var vehicle in vehicles)
            {
                var restultPictures = this.dbContext.Pictures.Where(x => x.Id == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }


                ViewVehiclesDto view = new ViewVehiclesDto()
                {
                    LotNumber = vehicle.Id,
                    Image = pictures[0],
                    Producer = vehicle.Producer,
                    ModelSpecifer = vehicle.ModelSpecifer,
                    ModelGeneration = vehicle.ModelGeneration,
                    RegistrationYear = vehicle.RegistrationYear,
                    MeterReadout = vehicle.MeterReadout,
                    DateTime = vehicle.DateTime,
                    CurrentBid = vehicle.CurrentBid,
                };

                viewVehicle.Add(view);

            }

            return viewVehicle;
        }

        public async Task<int> Create(CreateVehicleDto dto)
        {
            string stringFileName = UploadFile(dto);

            var picture = new Picture { PathImg = stringFileName };
            this.dbContext.Pictures.Add(picture);


            var vehicle = new Vehicle
            {
                Producer = dto.Producer,
                ModelSpecifer = dto.ModelSpecifer,
                ModelGeneration = dto.ModelGeneration,
                RegistrationYear = dto.RegistrationYear,
                Color = dto.Color,
                BodyType = dto.BodyType,
                Transmission = dto.Transmission,
                Drive = dto.Drive,
                MeterReadout = dto.MeterReadout,
                EngineCapacity = dto.EngineCapacity,
                EngineOutput = dto.EngineOutput,
                NumberKeys = dto.NumberKeys,
                ServiceManual = dto.ServiceManual,
                SecondTireSet = dto.SecondTireSet,
                LocationId = dto.LocationId,
                Fuel = dto.Fuel,
                PrimaryDamage = dto.PrimaryDamage,
                SecondaryDamage = dto.SecondaryDamage,
                VIN = dto.VIN,
                DateTime = dto.DateTime,
                SalesFinised = false
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

            var result = await this.dbContext
                .Vehicles
                .FindAsync(vehicle);

            return result.Id;
        }
        public async Task Delete(int id)
        {
            var result =  this.dbContext
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

        public async Task Update(EditVehicleDto dto)
        {
            var vehicle = await this.dbContext
                .Vehicles
                .FirstOrDefaultAsync(u => u.Id == dto.Id);

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

                var bind = new Bind()
                {
                    UserId = user.Id,
                    VehicleId = vehicle.Id,
                };

                /*
                if (this.dbContext.Binds.FirstOrDefault(x => x.UserId == user.Id && x.VehicleId == vehicle.Id) == null)
                {
                    this.dbContext.Binds.Add(bind);
                }
                */
                this.dbContext.Binds.Add(bind);

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
    }
}
