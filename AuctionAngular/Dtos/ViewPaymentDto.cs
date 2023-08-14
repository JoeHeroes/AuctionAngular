namespace AuctionAngular.Dtos
{
    public class ViewPaymentDto
    {
        public DateTime SaleDate { get; set; }
        public int LotId { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public int InvoiceAmount { get; set; }
        public DateTime LastInvoicePaidDate { get; set; }
        public DateTime LotLeftLocationDate { get; set; }
    }
}
