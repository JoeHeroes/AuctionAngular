
namespace Database.Entities
{
    public class Auction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public bool isStarted { get; set; }
        public bool isFinised { get; set; }
        public int LocationId { get; set; }
    }
}
