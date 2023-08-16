namespace AuctionAngular.Services.NewFolder
{
    public class InvoiceHeader
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int ReceiptNumber { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public int CustomerAddressId { get; set; }
        public int DeliveryAddressId { get; set; }
        public string Product { get; set; }
        public int Psc { get; set; }
        public int Tax { get; set; }
        public int Total { get; set; }
        public int NetTotal { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
