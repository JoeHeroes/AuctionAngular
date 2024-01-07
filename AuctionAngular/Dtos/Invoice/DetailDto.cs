namespace AuctionAngular.Dtos.Invoice
{
    public class DetailDto
    {
        public string Product { get; set; }
        public int Pcs { get; set; }
        public double Price { get; set; }
        public string Tax { get; set; }
        public double Total { get; set; }
    }
}
