
using AuctionAngular.Models;
using AuctionAngular.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace AuctionAngular.Services
{
    public interface IVehicleServices
    {
        int Create(VehicleDto dto);
        void Delete(int id);
        IEnumerable<Vehicle> GetAll();
        Vehicle GetById(int id);
        void Update(EditVehicleDto dto);
    }
    public class VehicleServices : IVehicleServices
    {
        private readonly AuctionDbContext dbContext;
        private readonly IWebHostEnvironment webHost;
        public VehicleServices(AuctionDbContext dbContext, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }


        private string UploadFile(VehicleDto dto)
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

        public Vehicle GetById(int id)
        {
            var vehicle = this.dbContext
                .Vehicles
                .FirstOrDefault(u => u.Id == id);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            return vehicle;
        }

        public IEnumerable<Vehicle> GetAll()
        {
            var vehicle = this.dbContext
                .Vehicles
                .ToList();

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            return vehicle;
        }

        public int Create(VehicleDto dto)
        {
            var stringFileName = UploadFile(dto);
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
                Location = dto.Location,
                Fuel = dto.Fuel,
                PrimaryDamage = dto.PrimaryDamage,
                SecondaryDamage = dto.SecondaryDamage,
                VIN = dto.VIN,
                ProfileImg = stringFileName,
                DateTime = dto.DateTime,
            };

            this.dbContext.Vehicles.Add(vehicle);
            this.dbContext.SaveChanges();

            var result = this.dbContext
                .Vehicles
                .Find(vehicle);

            return result.Id;
        }
        public void Delete(int id)
        {
            var vehicle = this.dbContext
                .Vehicles
                .FirstOrDefault(u => u.Id == id);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            this.dbContext.Vehicles.Remove(vehicle);
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public void Update(EditVehicleDto dto)
        {
            var vehicle = this.dbContext
                .Vehicles
                .FirstOrDefault(u => u.Id == dto.Id);

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
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

        }

    }
}
