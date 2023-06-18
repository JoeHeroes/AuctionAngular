
namespace Database.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Sent { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }

    }
}
