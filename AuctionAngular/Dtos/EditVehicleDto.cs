using System.ComponentModel;

namespace AuctionAngular.Dtos
{
    public class EditVehicleDto
    {
        public string Producer { get; set; }
        public string ModelSpecifer { get; set; }
        public string ModelGeneration { get; set; }
        public int RegistrationYear { get; set; }
        public string Color { get; set; }
        public string BodyType { get; set; }
        public int EngineCapacity { get; set; }
        public int EngineOutput { get; set; }
        public string Transmission { get; set; }
        public string Drive { get; set; }
        public long MeterReadout { get; set; }
        public string Fuel { get; set; }
        public int NumberKeys { get; set; }
        public bool ServiceManual { get; set; }
        public bool SecondTireSet { get; set; }
        public string PrimaryDamage { get; set; }
        public string SecondaryDamage { get; set; }
        public string VIN { get; set; } = null!;
        public string Highlights { get; set; }
        public string SaleTerm { get; set; }
        public int Auction { get; set; }

    }
}
