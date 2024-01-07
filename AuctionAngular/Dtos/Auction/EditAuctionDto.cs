namespace AuctionAngular.Dtos.Auction
{
    public class EditAuctionDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime AuctionDate { get; set; }
    }
}
