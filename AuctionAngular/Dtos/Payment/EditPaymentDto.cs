namespace AuctionAngular.Dtos.Payment
{
    public class EditPaymentDto
    {
        public int InvoiceAmount { get; set; }
        public DateTime LotLeftLocationDate { get; set; }
        public bool StatusSell { get; set; }
        public bool InvoiceGenereted { get; set; }
    }
}
