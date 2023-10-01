
namespace Database.Entities
{
    public class Auction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public bool isSalesStarted { get; set; }
        public bool isSalesFinised { get; set; }
        public int LocationId { get; set; }
    }
}
