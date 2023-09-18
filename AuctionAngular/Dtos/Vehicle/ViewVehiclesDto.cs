namespace AuctionAngular.Dtos.Vehicle
{
    public class ViewVehiclesDto
    {
        public int LotNumber { get; set; }
        public string Producer { get; set; }
        public string ModelSpecifer { get; set; }
        public string ModelGeneration { get; set; }
        public int RegistrationYear { get; set; }
        public long MeterReadout { get; set; }
        public DateTime DateTime { get; set; }
        public int CurrentBid { get; set; }
        public string Image { get; set; }
        public bool Sold { get; set; }
        public bool Confirm { get; set; }
    }
}
