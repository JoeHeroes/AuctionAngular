namespace AuctionAngular.Models
{
    public class Bid
    {
        public int BidId { get; set; }
        public bool BidStatus { get; set; }
        public int CurrentBid { get; set; }
        public bool SaleStatus { get; set; }
        public bool Watch { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
