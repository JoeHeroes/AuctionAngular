
namespace Database.Entities
{
    public class Auction
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public bool SalesStarted { get; set; }
        public bool SalesFinised { get; set; }
    }
}
