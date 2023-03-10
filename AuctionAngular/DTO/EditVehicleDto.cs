
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionAngular.Models.DTO
{
    public class EditVehicleDto
    {
        public int Id { get; set; }
        public int RegistrationYear { get; set; }
        public string Color { get; set; }
        public string BodyType { get; set; }
        public string Transmission { get; set; }
        public Location Location { get; set; }
        public string Fuel { get; set; }
        public DateTime DateTime { get; set; }


        public List<SelectListItem> RegistrationYearSelectList { get; set; }
        public List<SelectListItem> BodyTypeSelectList { get; set; }
        public List<SelectListItem> TransmissionSelectList { get; set; }
        public List<SelectListItem> FuelSelectList { get; set; }
        public List<SelectListItem> LocationSelectList { get; set; }
    }
}
