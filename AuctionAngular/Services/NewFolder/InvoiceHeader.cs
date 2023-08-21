namespace AuctionAngular.Services.NewFolder
{
    public class InvoiceHeader
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public int CustomerAddressId { get; set; }
        public int DeliveryAddressId { get; set; }
        public string Product { get; set; }
        public int Tax { get; set; }
        public int TaxFreePrice { get; set; }
        public int TaxTotal { get; set; }
        public int Total { get; set; }
        public string PaymentMethod { get; set; }
    }
}
