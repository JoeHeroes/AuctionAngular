namespace AuctionAngular.Dtos.Invoice
{
    public class ViewInvoicesDto
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public double Total { get; set; }
    }
}
