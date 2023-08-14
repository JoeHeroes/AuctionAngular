namespace AuctionAngular.Dtos
{
    public class EditPaymentDto
    {
        public int LocationId { get; set; }
        public string Description { get; set; }
        public int InvoiceAmount { get; set; }
        public DateTime LotLeftLocationDate { get; set; }
    }
}
