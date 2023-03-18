
using AuctionAngular.Models;
using AuctionAngular.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public interface IVehicleService
    {
        Task<int> Create(VehicleDto dto);
        Task Delete(int id);
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> GetById(int id);
        Task Update(EditVehicleDto dto);
    }
    public class VehicleService : IVehicleService
    {
        private readonly AuctionDbContext dbContext;
        private readonly IWebHostEnvironment webHost;
        public VehicleService(AuctionDbContext dbContext, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }


        private async Task<string> UploadFile(VehicleDto dto)
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

        public async Task<Vehicle> GetById(int id)
        {
            var result = await this.dbContext
                .Vehicles
                .FirstOrDefaultAsync(u => u.Id == id);

            if (result is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            return result;
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            var result = await this.dbContext
                .Vehicles
                .ToListAsync();

            if (result is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            return result;
        }

        public async Task<int> Create(VehicleDto dto)
        {
            var stringFileName = await UploadFile(dto);
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
                Fuel = dto.Fuel,
                PrimaryDamage = dto.PrimaryDamage,
                SecondaryDamage = dto.SecondaryDamage,
                VIN = dto.VIN,
                ProfileImg = stringFileName,
                DateTime = dto.DateTime,
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
            vehicle.Location = dto.Location;
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
    }
}
