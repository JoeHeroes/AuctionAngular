using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuctionAngular.Models.DTO
{
    public class VehicleDto
    {
        [Required]
        public string Producer { get; set; }

        [Required]
        [DisplayName("Model Specifer")]
        public string ModelSpecifer { get; set; }

        [Required]
        [DisplayName("Model Generation")]
        public string ModelGeneration { get; set; }

        [Required]
        [DisplayName("Registration Year")]
        public int RegistrationYear { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        [DisplayName("Body Type")]
        public string BodyType { get; set; }
       
        [Required]
        public string Transmission { get; set; }

        [Required]
        public string Drive { get; set; }

        [Required]
        [DisplayName("Meter Readout")]
        public long MeterReadout { get; set; }

        [Required]
        public string Fuel { get; set; }
       
        [Required]
        [DisplayName("Primary Damage")]
        public string PrimaryDamage { get; set; }
        [Required]
        [DisplayName("Secondary Damage")]
        public string SecondaryDamage { get; set; }
        
        [Required]
        [DisplayName("Engine Capacity")]
        public int EngineCapacity { get; set; }

        [Required]
        [DisplayName("Engine Output")]
        public int EngineOutput { get; set; }

        [Required]
        [DisplayName("Number Keys")]

        public string NumberKeys { get; set; }
        
        [Required]
        [DisplayName("Service Manual")]
        public bool ServiceManual { get; set; }
        
        [Required]
        [DisplayName("Second Tire Set")]
        public bool SecondTireSet { get; set; }

        [Required]
        public string VIN { get; set; } = null!;
        public IFormFile PathPic { get; set; }

        [Required]
        [DisplayName("Date of Auction")]
        public DateTime DateTime { get; set; }



    }
}
