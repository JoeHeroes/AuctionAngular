using System.ComponentModel;

namespace Database.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Producer { get; set; }
        [DisplayName("Model Specifer")]
        public string ModelSpecifer { get; set; }
        [DisplayName("Model Generation")]
        public string ModelGeneration { get; set; }
        [DisplayName("Registration Year")]
        public int RegistrationYear { get; set; }
        public string Color { get; set; }
        [DisplayName("Body")]
        public string BodyType { get; set; }
        [DisplayName("Engine Capacity")]
        public int EngineCapacity { get; set; }
        [DisplayName("Engine Output")]
        public int EngineOutput { get; set; }
        public string Transmission { get; set; }
        public string Drive { get; set; }
        [DisplayName("Meter Readout")]
        public long MeterReadout { get; set; }
        public string Fuel { get; set; }
        [DisplayName("Number Keys")]
        public int NumberKeys { get; set; }
        [DisplayName("Service Manual")]
        public bool ServiceManual { get; set; }
        [DisplayName("Second Tire Set")]
        public bool SecondTireSet { get; set; }
        public string PrimaryDamage { get; set; }
        public string SecondaryDamage { get; set; }
        public string VIN { get; set; } = null!;
        public string Highlights { get; set; }
        [DisplayName("Sale Term")]
        public string SaleTerm { get; set; }
        public int CurrentBid { get; set; }
        public int WinnerId { get; set; }
        public int AuctionId { get; set; }
        public bool Sold { get; set; }
        public bool Confirm { get; set; }
    }
}
