namespace AuctionAngular.Dtos
{
    public class EditEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Color { get; set; }
        public bool AllDay { get; set; }
    }
}
