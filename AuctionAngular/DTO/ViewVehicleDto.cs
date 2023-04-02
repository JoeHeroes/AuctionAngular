using AuctionAngular.Enum;
using System.ComponentModel;

namespace AuctionAngular.DTO
{
    public class ViewVehicleDto
    {
        public int Id { get; set; }
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
        public string NumberKeys { get; set; }
        public bool ServiceManual { get; set; }
        public bool SecondTireSet { get; set; }
        public int LocationId { get; set; }
        public string PrimaryDamage { get; set; }
        public string SecondaryDamage { get; set; }
        public string VIN { get; set; } = null!;
        public Highlight Highlights { get; set; }
        public DateTime DateTime { get; set; }
        public int CurrentBid { get; set; }
        public int WinnerId { get; set; }
        public List<string> Images { get; set; }
    }
}
