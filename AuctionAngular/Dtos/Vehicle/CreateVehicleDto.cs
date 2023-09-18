using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.Dtos.Vehicle
{
    public class CreateVehicleDto
    {
        [Required]
        public string Producer { get; set; }

        [Required]
        public string ModelSpecifer { get; set; }

        [Required]
        public string ModelGeneration { get; set; }

        [Required]
        public int RegistrationYear { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public int AuctionId { get; set; }

        [Required]
        public string BodyType { get; set; }

        [Required]
        public string Transmission { get; set; }

        [Required]
        public string Drive { get; set; }

        [Required]
        public long MeterReadout { get; set; }

        [Required]
        public string Fuel { get; set; }

        [Required]
        public string PrimaryDamage { get; set; }
        [Required]
        public string SecondaryDamage { get; set; }

        [Required]
        public int EngineCapacity { get; set; }

        [Required]
        public int EngineOutput { get; set; }

        [Required]
        public int NumberKeys { get; set; }

        [Required]
        public bool ServiceManual { get; set; }

        [Required]
        public bool SecondTireSet { get; set; }

        [Required]
        public string VIN { get; set; } = null!;

        [Required]
        public string SaleTerm { get; set; }
        [Required]
        public string Highlights { get; set; }

    }
}
