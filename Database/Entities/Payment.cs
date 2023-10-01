
namespace Database.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public string Description { get; set; }
        public int InvoiceAmount { get; set; }
        public DateTime LastInvoicePaidDate { get; set; }
        public DateTime LotLeftLocationDate { get; set; }
        public bool isSold { get; set; }
        public bool isInvoiceGenereted { get; set; }
        public int LotId { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
    }
}
