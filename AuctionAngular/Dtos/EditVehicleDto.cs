namespace AuctionAngular.Dtos
{
    public class EditVehicleDto
    {
        public int RegistrationYear { get; set; }
        public string Color { get; set; }
        public string BodyType { get; set; }
        public string Transmission { get; set; }
        public int LocationId { get; set; }
        public string Fuel { get; set; }
        public DateTime DateTime { get; set; }

    }
}
