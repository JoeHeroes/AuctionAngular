namespace AuctionAngular.Dtos.Payment
{
    public class CreatePaymentDto
    {
        public int LotId { get; set; }
        public int AuctionId { get; set; }
        public string Description { get; set; }
        public int InvoiceAmount { get; set; }
        public DateTime LotLeftLocationDate { get; set; }
    }
}
