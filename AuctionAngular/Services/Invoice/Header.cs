namespace AuctionAngular.Services.Invoice
{
    public class Header
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public int CustomerAddressId { get; set; }
        public int DeliveryAddressId { get; set; }
        public string Product { get; set; }
        public double Tax { get; set; }
        public double TaxFreePrice { get; set; }
        public double TaxTotal { get; set; }
        public double Total { get; set; }
        public string PaymentMethod { get; set; }
    }
}
