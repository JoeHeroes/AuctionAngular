namespace AuctionAngular.Dtos
{
    public class ViewPaymentDto
    {
        public DateTime SaleDate { get; set; }
        public int LotId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int InvoiceAmount { get; set; }
        public DateTime LastInvoicePaidDate { get; set; }
        public DateTime LotLeftLocationDate { get; set; }
        public bool StatusSell { get; set; }
        public bool InvoiceGenereted { get; set; }
    }
}
