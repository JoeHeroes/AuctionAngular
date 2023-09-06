namespace AuctionAngular.Dtos
{
    public class ViewAuctionDto
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }
        
        public string Description { get; set; }

        public string Location { get; set; }

        public int CountVehicle { get; set; }

    }
}
