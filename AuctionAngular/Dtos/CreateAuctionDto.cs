namespace AuctionAngular.Dtos
{
    public class CreateAuctionDto
    {
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime AuctionDate { get; set; }
    }
}
